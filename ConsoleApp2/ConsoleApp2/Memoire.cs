
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    [Serializable]
    public abstract class Memoire
    {
        private int taille;
        //Construteur
        public Memoire(int taille)
        {
            this.taille = taille;
        }

        //Getters
        public int GetTaille()
        {
            return taille;
        }
    }
}
