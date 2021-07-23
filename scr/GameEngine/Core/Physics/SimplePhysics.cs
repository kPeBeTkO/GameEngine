using GameEngine.Core.Collisions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Core.Physics
{
    public class SimplePhysics : IPhysics
    {
        public void MoveObject(GameObject obj)
        {
            if (!(obj is Entity))
                return;
            var entity = (Entity)obj;
            /*if (entity.Speed == null || entity.Speed.Length == 0)
                return;*/
            entity.Body.Location += entity.Speed;
            if (!entity.Collidable)
                return;
            var offset = new Vector(0, 0);
            foreach (var other in Core.Objects)
            {
                if (other == entity)
                    continue;
                if (other.Collidable && Body.CheckCollision(entity.Body, other.Body))
                {
                    offset += GetOffsetFromBody(entity.Body, other.Body);
                }
            }
            entity.Body.Location += offset;
        }

        public Vector OffsetFromCircle(Body entity, Circle circle)
        {
            var normalPos = entity.ClosestPointFrom(circle.Location);
            var normal = normalPos - circle.Location;
            return normal * ((circle.Radius - normal.Length) / normal.Length);
        }

        /*public Vector OffsetFromBox(Body entity, Box box)
        {
            var normalPos = entity.ClosestPointFrom(box.Location);
            var normal = normalPos - box.Location;
        }*/

        public Vector GetOffsetFromBody(Body entity, Body body)
        {
            var deepest = entity.ClosestPointFrom(body.Location);
            var outside = body.ClosestPointFrom(deepest);
            return outside - deepest; 
        }
    }
}
