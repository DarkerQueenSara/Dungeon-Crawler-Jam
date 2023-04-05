using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{


    public class IdleZombie : BaseState
    {
        
        public LayerMask player;
        public LayerMask obstacles;
        
        
        public float angle;
        public float radius;

        public ChaseZombie chaseZombie;

        public bool isInRange = false;

        // Start is called before the first frame update
        public override BaseState RunState(Vector3 playerPos)
        {
            DetectPlayer();
            if (isInRange)
            {
                return chaseZombie;
            }

            return this;
        }
        
          private void DetectPlayer()
          {
              Collider[] targets = Physics.OverlapSphere(transform.position, radius, player);

              if (targets.Length != 0)
              {
                  Transform target = targets[0].transform;
                  Vector3 directionToTarget = (target.position - transform.position).normalized;

                  if (Vector3.Angle(transform.position, directionToTarget) < angle / 2)
                  {
                      float distanceToTarget = Vector3.Distance(transform.position, target.position);

                      if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacles))
                      {
                          isInRange = true;
                      }
                      else
                      {
                          isInRange = false;
                      }
                  }
                  else
                  {
                      isInRange = false;
                  }
              }
              
              else if (isInRange)
              {
                  isInRange = false;
              }
          }


    }
}