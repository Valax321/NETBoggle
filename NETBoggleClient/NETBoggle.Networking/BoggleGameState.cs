using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETBoggle.Networking
{
    public interface IBoggleState
    {
        IBoggleState Handle(Server cur_s);
    }

    public class BoggleWaitReady : IBoggleState
    {
        public IBoggleState Handle(Server cur_s)
        {
            bool all_ready = true;

            foreach (Player p in cur_s.Players)
            {
                if (!p.Ready)
                {
                    all_ready = false;
                }
            }

            if (!all_ready)
            {
                return null;
            }

            return new BogglePlay();
        }
    }

    public class BogglePlay : IBoggleState
    {
        public float CurTime = Server.GameLength;

        public IBoggleState Handle(Server cur_s) //Fires every tick.
        {
            if (CurTime <= 0)
            {
                //Game ends
                return new BoggleRoundEnd();
            }
            else
            {
                CurTime -= cur_s.DeltaTime;
                return null;
            }
        }
    }

    public class BoggleRoundEnd : IBoggleState
    {
        public IBoggleState Handle(Server cur_s)
        {
            throw new NotImplementedException();
        }
    }
}
