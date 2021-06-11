using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Password_Project.Model
{
    public class ProceduralNumber
    {
        private CustomLinkedList ProceduralList { get; set; }
        private string _digest;
        public ProceduralNumber(string digest)
        {
            _digest = digest;
            ProceduralList = new CustomLinkedList();
            foreach (var ch in digest)
            {
                ProceduralList.Add(Convert.ToInt32(ch.ToString(), 16));
            }
            ProceduralList.Iterator = ProceduralList.Head;
        }

        public int Generate(int reqLength)
        {
            string accumulator = "";
            int powerFactor = reqLength.ToString().Length;

            for (int i = 0; i < powerFactor; i++)
            {
                GoNext();
                accumulator = $"{accumulator}{GetCurrent()}";
            }

            return Int32.Parse(accumulator);
        }

        private void GoNext()
        {
            if (ProceduralList.Iterator.Next.IsNullOrWhiteSpace())
            {
                _digest = StaticMethods.Hash(_digest);
                var tmpList = new ProceduralNumber(_digest);
                ProceduralList.Iterator.Next = tmpList.ProceduralList.Head;
            }
            else
            {
                ProceduralList.Iterator = ProceduralList.Iterator.Next;
            }
        }

        public int GetCurrent()
        {
            return (int)ProceduralList.Iterator.Data;
        }
    }
}
