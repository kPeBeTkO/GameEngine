using GameEngine.Logic.Collisions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Logic.Physics
{
    public class SimplePhysics : IPhysics
    {
        public void MoveObject(GameObject obj)
        {
            if (!(obj is Entity))
                return;
            var entity = (Entity)obj;
            if (entity.Speed == null || entity.Fixed)
                return;
            entity.Body.Location += entity.Speed;
            if (!entity.Solid)
                return;
            var offset = new Vector(0, 0);
            foreach(var other in Core.Objects)
            {
                if (other == entity)
                    continue;
                if (other.Solid && Body.CheckCollision(entity.Body, other.Body)) 
                {
                    offset += GetOffsetFromBody(entity.Body, other.Body);
                }
            }
            entity.Body.Location += offset;
        }

        public Vector GetOffsetFromBody(Body entity, Body body)
        {
            if (entity is Box)
            {
                var deepest = entity.ClosestPointFrom(body.Location);
                if (body.IsInside(deepest))
                {
                    var outside = body.ClosestPointFrom(deepest);
                    return outside - deepest;
                }
            }
            else
            {
                var deepest = body.ClosestPointFrom(entity.Location);
                if (entity.IsInside(deepest))
                {
                    var outside = entity.ClosestPointFrom(deepest);
                    return deepest - outside;
                }
            }
            return new Vector(0, 0);
        }
    }
}
