using MahJong.GameObject;
using MahJong.GameObject.Enemies;
using MahJong.GameObject.Mario;
using MahJong.GameObject.Obstacles;
using MahJong.TileMap;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Posteriori Collision Detection
namespace MahJong.Collision
{
    enum Direction { Top, Bottom, Left, Right }

    class CollisionPairs : IComparable<CollisionPairs>
    {
        public Direction dir;       // the side obj1 collide with obj2
        public float time;
        public IGameObject obj1;
        public IGameObject obj2;

        public CollisionPairs(Direction dir, IGameObject obj1, IGameObject obj2, float time)
        {
            this.dir = dir;
            this.obj1 = obj1;
            this.obj2 = obj2;
            this.time = time;
        }

        public int CompareTo(CollisionPairs other)
        {
            return (int) (time - other.time);
        }
    }

    internal class CollisionDetect
    {
        protected Map Map;

        public CollisionDetect(Map map)
        {
            Map = map;
        }

        private static Boolean CollidingExactlyOnBottom(IGameObject a, IGameObject b)
        {
            return a.Boundary.Bottom == b.Boundary.Top && a.Boundary.Top < b.Boundary.Top &&
                a.Boundary.Right > b.Boundary.Left && a.Boundary.Left < b.Boundary.Right;
        }

        private static float GetCollideTime(Rectangle r, Vector2 v, Direction dir)
        {
            if (dir == Direction.Bottom || dir == Direction.Top)
                return v.Y == 0.0f ? 0 : Math.Abs(r.Height / v.Y);

            return v.X == 0.0f ? 0 : Math.Abs(r.Width / v.X);
        }

        private static void UpdateFirstCollision(List<CollisionPairs> pairs, HashSet<IGameObject> possibleCollision, 
            HashSet<Tuple<IGameObject, IGameObject>> seen, bool[] grounded)
        {            
            pairs.Sort();
            float time = pairs[0].time;

            foreach (CollisionPairs c in pairs)
            {
                if (c.time == time)
                {
                    if (c.dir == Direction.Bottom)
                    {
                        c.obj2.CollideOnTop(c.obj1);
                        c.obj1.CollideOnBottom(c.obj2);
                        if (!(c.obj2 is IBlock && (c.obj2 as IBlock).IsHidden))
                            grounded[0] = true;
                    }
                    else if (c.dir == Direction.Top)
                    {
                        c.obj2.CollideOnBottom(c.obj1);
                        c.obj1.CollideOnTop(c.obj2);
                    }
                    else if (c.dir == Direction.Left)
                    {
                        c.obj2.CollideOnRight(c.obj1);
                        c.obj1.CollideOnLeft(c.obj2);
                    }
                    else if (c.dir == Direction.Right)
                    {
                        c.obj2.CollideOnLeft(c.obj1);
                        c.obj1.CollideOnRight(c.obj2);
                    }

                    seen.Add(new Tuple<IGameObject, IGameObject> (c.obj2, c.obj1));
                }
                else
                    possibleCollision.Add(c.obj2);            
            }
        }

        private List<CollisionPairs> GetCollidedPairs(IGameObject obj1, HashSet<IGameObject> possibleCollision, 
            HashSet<Tuple<IGameObject, IGameObject>> seen)
        {
            List<CollisionPairs> collided = new List<CollisionPairs>();

            foreach (IGameObject obj2 in possibleCollision)
            {
                Rectangle intersect = Rectangle.Intersect(obj1.Boundary, obj2.Boundary);
                bool CollideOnBottom = CollidingExactlyOnBottom(obj1, obj2);

                if (intersect != null || CollideOnBottom)
                {
                    Tuple<IGameObject, IGameObject> temp = new Tuple<IGameObject, IGameObject>(obj1, obj2);

                    if (!seen.Contains(temp))
                    {
                        if ((intersect.Bottom == obj2.Boundary.Bottom && intersect.Width > intersect.Height))
                            collided.Add(new CollisionPairs(Direction.Top, obj1, obj2, GetCollideTime(intersect, obj1.Velocity, Direction.Top)));
                        else if ((intersect.Top == obj2.Boundary.Top && intersect.Width > intersect.Height) || CollideOnBottom)
                            collided.Add(new CollisionPairs(Direction.Bottom, obj1, obj2, GetCollideTime(intersect, obj1.Velocity, Direction.Bottom)));
                        else if ((intersect.Right == obj2.Boundary.Right && intersect.Width < intersect.Height))
                            collided.Add(new CollisionPairs(Direction.Left, obj1, obj2, GetCollideTime(intersect, obj1.Velocity, Direction.Left)));
                        else if ((intersect.Left == obj2.Boundary.Left && intersect.Width < intersect.Height))
                            collided.Add(new CollisionPairs(Direction.Right, obj1, obj2, GetCollideTime(intersect, obj1.Velocity, Direction.Right)));
                    }
                }
            }

            return collided;
        }

        public void Update()
        {           
            HashSet<Tuple<IGameObject, IGameObject>> seen = new HashSet<Tuple<IGameObject, IGameObject>> ();
            HashSet<IGameObject> moving = new HashSet<IGameObject>(Map.MovingObject);

            foreach (IGameObject obj1 in moving)
            {
                HashSet<IGameObject> possibleCollision = new HashSet<IGameObject>();                
                bool[] grounded = { false };

                if (Map.Position.ContainsKey(obj1))
                    foreach (Point p in Map.Position[obj1])
                        foreach (IGameObject o in Map.TileMap[p.X][p.Y])
                            if (!o.Equals(obj1))
                                possibleCollision.Add(o);

                while (possibleCollision.Count != 0)
                {
                    List<CollisionPairs> collided = GetCollidedPairs(obj1, possibleCollision, seen);
                    if (collided.Count == 0)
                        break;

                    possibleCollision = new HashSet<IGameObject> ();
                    UpdateFirstCollision(collided, possibleCollision, seen, grounded);
                }

                obj1.Grounded = grounded[0];                                 
            }
        }
    }
}
