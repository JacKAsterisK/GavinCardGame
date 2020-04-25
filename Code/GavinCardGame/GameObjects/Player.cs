using GavinCardGame.GameObjects.SceneObjects;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static GavinCardGame.GSystems;

namespace GavinCardGame.GameObjects
{
    public class Player : ObjBase
    {
        #region Net Specs

        public struct CreateSpec
        {
            public int Id;
            public bool IsServer;
            public (int id, int cardId)[] MercCards;
            public (int id, int cardId)[] KfCards;
            public int TestCardId;

            public void Apply(Player player)
            {
                player.IsServer = IsServer;
                player.TestCard = GScene.Create<MercCard>(player, TestCardId);
            }

            public static void Serialize(Player player, NetBuffer buff)
            {
                // Id
                buff.Write(player.Id);

                // IsServer
                buff.Write(player.IsServer);

                // Merc cards
                buff.Write(player.MercCards.Count);
                foreach (var _card in player.MercCards)
                {
                    buff.Write(_card.Id);
                    buff.Write(_card.CardId);
                }

                // KF cards
                buff.Write(player.KfCards.Count);
                foreach (var _card in player.KfCards)
                {
                    buff.Write(_card.Id);
                    buff.Write(_card.CardId);
                }

                // Test card Id
                buff.Write(player.TestCard.Id);
            }

            public static CreateSpec Deserialize(NetIncomingMessage message)
            {
                CreateSpec _spec = new CreateSpec();

                // Id
                _spec.Id = message.ReadInt32();

                // IsServer
                _spec.IsServer = message.ReadBoolean();

                // Merc cards
                int _mercCount = message.ReadInt32();
                _spec.MercCards = new (int id, int cardId)[_mercCount];
                for (int _index = 0; _index < _mercCount; _index++)
                    _spec.MercCards[_index] = (message.ReadInt32(), message.ReadInt32());

                // KF cards
                int _kfCount = message.ReadInt32();
                _spec.KfCards = new (int id, int cardId)[_kfCount];
                for (int _index = 0; _index < _kfCount; _index++)
                    _spec.KfCards[_index] = (message.ReadInt32(), message.ReadInt32());

                // Test card Id
                _spec.TestCardId = message.ReadInt32();

                return _spec;
            }
        }

        public struct UpdateSpec
        {
            public int Id;
            public Vector2 TestCardPos;

            public void Apply(Player player)
            {
                player.TestCard.Position = TestCardPos;
            }

            public static void Serialize(Player player, NetBuffer buffer)
            {
                // Id
                buffer.Write(player.Id);

                // Test card position
                buffer.Write(player.TestCard.Position.X);
                buffer.Write(player.TestCard.Position.Y);
            }

            public static UpdateSpec Deserialize(NetIncomingMessage message)
            {
                UpdateSpec _spec = new UpdateSpec();

                // Id
                _spec.Id = message.ReadInt32();

                // Test card position
                _spec.TestCardPos.X = message.ReadFloat();
                _spec.TestCardPos.Y = message.ReadFloat();

                return _spec;
            }
        }

        #endregion

        public bool IsServer { get; set; }
        public bool IsMe { get { return GNet.IsServer ? IsServer : !IsServer; } }

        public List<MercCard> MercCards { get; set; }
        public List<KfCard> KfCards { get; set; }

        public MercCard TestCard { get; set; }

        public Player(ObjBase parent, int? id) : base(parent, id)
        {
            MercCards = new List<MercCard>();
            KfCards = new List<KfCard>();

            if (GNet.IsServer)
            {
                TestCard = GScene.Create<MercCard>(this);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsMe)
            {
                TestCard.Position = GInput.MousePos;

                var _msg = GNet.CreateMessage();
                _msg.Write((byte)Systems.ObjectType.Player);
                UpdateSpec.Serialize(this, _msg);
                GNet.SendMessage(Systems.MessageType.UpdateObject, _msg);
            }
            
            TestCard.Color = IsServer ? Color.Aqua : Color.Red;
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);
        }
    }
}
