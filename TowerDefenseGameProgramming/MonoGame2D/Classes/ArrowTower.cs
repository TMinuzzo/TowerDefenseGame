using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2D
{
	public class ArrowTower : Tower
	{
		/* Constructor */
        public ArrowTower(Texture2D texture, Texture2D bulletTexture, Vector2 position)
            : base(texture, bulletTexture, position)
        {
            this.cost = Constants.ARROW_TOWER_COST;
            this.damage = Constants.ARROW_TOWER_DAMAGE;
            this.radius = Constants.ARROW_TOWER_RADIUS;
			this.cost = Constants.ARROW_TOWER_COST;
			this.bulletSpeed = Constants.ARROW_TOWER_BULLET_SPEED;
        }

		/* Others */
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (bulletTimer >= 0.75f && target != null)
            {
                Bullet bullet = new Bullet(bulletTexture, Vector2.Subtract(GetCenter(),
                    new Vector2(bulletTexture.Width / 2)), rotation, bulletSpeed, damage);

				bulletList = bulletList.Add(bullet);
                bulletTimer = 0;
            }

            for (int i = 0; i < bulletList.Count; i++)
            {
                Bullet bullet = bulletList[i];

                bullet.SetRotation(rotation);
                bullet.Update(gameTime);

                if (!IsInRange(bullet.GetCenter()))
                    bullet.Kill();

                if (target != null && Vector2.Distance(bullet.GetCenter(), target.GetCenter()) < 12)
                {
					float newCurrentHealth = target.GetCurrentHealth() - bullet.GetDamage();
					target.SetCurrentHealth(newCurrentHealth);
                    bullet.Kill();
                }

                if (bullet.IsDead())
                {
					bulletList = bulletList.Remove(bullet);
                    i--;                  
                    }
                }

        }
    }
}
