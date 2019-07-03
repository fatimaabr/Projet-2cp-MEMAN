
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    [Serializable]
    public abstract class syslfuaging : PageReplacmment
    {
        /*ATRIBUTS*/
        public TableDePageLFUAging TablePage { get; set; }
        public int DernierePage { get; set; }

        public syslfuaging(int tailleMV, int tailleMP, int taillePageCase) : base(tailleMP, taillePageCase)
        {
            DernierePage = -1;
            //création de la table de pages en mettant à jour le nombre d'entrées
            TablePage = new TableDePageLFUAging(tailleMV / taillePageCase);
        }

        public abstract void MettreTableAjour();

        /// <summary>
        /// La méthode retourne le numéro de page ayant le compteur le plus petit
        /// </summary>
        /// <returns></returns>
        public override int PageAReplacer()
        {
            return TablePage.CalculPagePetitCompteur();
        }


        /// <summary>
        /// 
        /// </summary>
        public override abstract string DeroulerAlgorithme();
      
    }
}