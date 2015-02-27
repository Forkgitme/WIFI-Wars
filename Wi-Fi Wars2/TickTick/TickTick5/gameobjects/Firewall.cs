using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    class Firewall : AnimatedGameObject
    {
        public Firewall()
            : base(1, "firewall")
        {
            this.LoadAnimation("Sprites/firewall_burning", "burning", true, 0.2f);
            this.LoadAnimation("Sprites/firewall_idle", "idle", true, 0.2f);
            this.PlayAnimation("burning");
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
