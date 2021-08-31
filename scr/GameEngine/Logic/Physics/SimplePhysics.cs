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
                    var bodyOffset = GetOffsetFromBody(entity.Body, other.Body);
                    if (bodyOffset.IsValid())
                        offset += bodyOffset;
                }
            }
            entity.Body.Location += offset;
        }

        public Vector GetOffsetFromBody(Body entity, Body body)
        {
            if (entity is Box)
            {
                if (!entity.IsInside(body.Location))
                {
                    var deepest = entity.ClosestPointFrom(body.Location);
                    var outside = body.ClosestPointFrom(deepest);
                    return outside - deepest;
                }
                else
                {
                    //недоделано
                    var e = (Box)entity;
                    var dist = e.Location - body.Location;
                    var closest = body.ClosestPointFrom(e.Location);
                    var dist2 = closest - body.Location;
                    if (e.Height / 2 - Math.Abs(dist.Y) < e.Width / 2 -Math.Abs(dist.X))
                    {
                        return new Vector(0, Math.Sign(dist.Y) * (e.Height / 2 + Math.Abs(dist2.Y) - Math.Abs(dist.Y)));
                    }
                    else
                        return new Vector(Math.Sign(dist.X) * (e.Width / 2 + Math.Abs(dist2.X) - Math.Abs(dist.X)), 0);
                }
            }
            else
            {
                if (!entity.IsInside(body.Location))
                {
                    var deepest = body.ClosestPointFrom(entity.Location);
                    var outside = entity.ClosestPointFrom(deepest);
                    return deepest - outside;
                }
                else
                {
                    //недоделано
                }
            }
            return new Vector(0, 0);
        }
    }
}
