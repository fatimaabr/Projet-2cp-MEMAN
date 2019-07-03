using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2

{
    [Serializable]
    public class SystemeAging : syslfuaging
    {
        const uint v = 0b_10000000;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="tailleMV">Taille de la mémoire virtuelle</param>
        /// <param name="tailleMP">Taille de la mémoire physique</param>
        /// <param name="taillePageCase">Taille de la page/case</param>
        public SystemeAging(int tailleMV, int tailleMP, int taillePageCase) : base(tailleMV, tailleMP, taillePageCase) { }

        /// <summary>
        /// Mettre à jour la table de page: méthode appelée à chaque top d'horloge
        /// </summary>
        public override void MettreTableAjour()
        {
            int j = 0;
            if (DernierePage != -1)
            {
                while (j < TablePage.nbEntrees)
                {
                    //décalage du compteur d'une position vers la droite
                    TablePage.ListeTB[j].Compteur = TablePage.ListeTB[j].Compteur / 2;
                    j++;
                }
                //Pour la dernière page utilisée, mettre à jour le compteur en ajoutant Ri au bit de poids fort
                TablePage.ListeTB[DernierePage].Compteur = TablePage.ListeTB[DernierePage].Compteur | v; //ou logique avec "10000000"
                //Remettre Ri à 0
                TablePage.ListeTB[DernierePage].Ri = 0;
            }
        }
        public override string DeroulerAlgorithme()
        {
            string[] arr = new string[4];
            int pageAR;
            //parcourir la liste de page 
            //while (ConditionContinuer())
            //Récupérer la tête de la liste entrée par l'utilisateur
            PageCase pageCourante = GetListei(0);
            //Liberer la tete de la liste
            SuppDeListe(0);
            //Sauvegarder le numéro de la page
            DernierePage = pageCourante.numeroPage;

            if (MemoirePhysique.MemoireVide)
            {
                TablePage.PagePetitCompteur = pageCourante.numeroPage;
            }
            //Mettre le Ri de la page courante à 1
            TablePage.ListeTB[pageCourante.numeroPage].Ri = 1;
            //Si la page n'est pas présente en mémoire
            if (!PageExiste(pageCourante.GetNumeroPage()))
            {
                //********************************************************************************************
                String pg = Convert.ToString(pageCourante.GetNumeroPage());
                arr[0] = " La page" + " " + pg + " " + "n’existe pas en mémoire (défaut de page ) ";
                //********************************************************************************************
                //incrémenter le nombre de défauts de page
                SetDefautPages(GetDefautPage() + 1);
                //Si la mémoire physique n'est pas entièrement pleine
                if (!MemoirePleine())
                {
                    //**********************************************************************************
                    arr[1] = " et la mémoire n’est pas pleine :" + "\n" + "-	la page" + " " + pg + " " + " est chargée dans la première case vide." + "\n" + "	Dans la table de page :" + "\n" + "- Bit de présence de la page chargée en mémoire mis a  1." + "\n" + "- Mettre le registre Compteur Ci a 1(a droite)« 1000 0000 »" + "\n" + "- Décalage a droite de tout les Ri(Registre de référence)." + "\n" + "- Rajouter Ri au Ci." + "\n" + "-	Mettre a jour la case correspondante a la page.";
                    //*********************************************************************************
                    //Charger la page à la première case vide
                    pageCourante.SetNumeroCase(PremiereCaseLibre());
                    //Mettre à jour le numéro de case de la page -dans la table de page-
                    TablePage.ListeTB[pageCourante.numeroPage].NumeroCase = PremiereCaseLibre();
                    RemplacerDansMemoire(pageCourante, PremiereCaseLibre());
                    //Décrémenter le nombre de cases libres en mémoire physique
                    DecCasesLibre();
                }
                else
                {
                    //******************************************************************************
                    //******************************************************************************
                    pageCourante.SetNumeroCase(PageAReplacer());
                    //Mettre à jour le numéro de case de la page -dans la table de page-
                    TablePage.ListeTB[pageCourante.numeroPage].NumeroCase = TablePage.ListeTB[PageAReplacer()].NumeroCase;
                    pageAR = PageAReplacer();

                    //specifier le numero de page a remplacer et le rajouter dans la chaine retourner en sortie 
                    String pr = Convert.ToString(PageAReplacer());
                    arr[1] = "et la mémoire est  pleine" + "\n" + "Remplacement de la page selon AGING:" + "\n" + "- La page qui a le Ci le plus petit est  remplacée.("+pr+")"+ "\n" + "Dans la table de page:"+"\n"+ " -Bit de présence de la page chargée en mémoire mis a  1" +"\n"+ "- Mettre le registre Compteur Ci a 1(a droite)« 1000 0000 »" + "\n" + "- Décalage a droite de tout les Ri(Registre de référence)." + "\n" + "- Rajouter Ri au Ci." + "\n" + "- Mettre a jour la case correspondante a la page.";

                    TablePage.ListeTB[pageAR].NumeroCase = -1;
                    //TablePage.ListeTB[pageAR].Compteur = 0;
                    RemplacerDansMemoire(pageCourante, TablePage.ListeTB[PageAReplacer()].NumeroCase);
                }
            }
            else
            {
                //***********************************************************************************
             //**************************************************************************************
             String pag = Convert.ToString(pageCourante.GetNumeroPage());
                arr[0] = " La page" + " " + pag + " " + "existe déjà en mémoire "+"\n"+ "	Dans la table de page:"+ "\n"+ "-Mettre le registre Compteur Ci a 1(a droite)« 1000 0000 »" + "\n" + "-Décalage a droite de tout les Ri(Registre de référence)." + "\n" + "-Rajouter Ri au Ci.";
            }
            string result = arr[0] + " " + arr[1];
            return result;
        }
    }
}

