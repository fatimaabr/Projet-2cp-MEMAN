
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2

{
   
    public class SystemeLFU : syslfuaging
    {
        /// <summary>
        /// Constucteur
        /// </summary>
        /// <param name="tailleMV">Taille de la mémoire virtuelle</param>
        /// <param name="tailleMP">Taille de la mémoire physique</param>
        /// <param name="taillePageCase">Taille de la Page/Case</param>
        public SystemeLFU(int tailleMV, int tailleMP, int taillePageCase) : base(tailleMV, tailleMP, taillePageCase) { }


        /// <summary>
        /// Mettre à jour la table de page: méthode appelée à chaque top d'horloge
        /// </summary>
        public override void MettreTableAjour()
        {
            //Incrémenter les compteurs avec Ri à 1
            if (DernierePage != -1)
            {
                TablePage.ListeTB[DernierePage].Ri = 0;
                TablePage.ListeTB[DernierePage].Compteur++;
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
                //*******************************************************************************************
                //  => la page n'existe pas 
                String pg = Convert.ToString(pageCourante.GetNumeroPage());
                arr[0] = "1. La page" + " " + pg + " " + "n’existe pas en mémoire (défaut de page ) ";
         
               
                //********************************************************************************************
                //incrémenter le nombre de défauts de page
                SetDefautPages(GetDefautPage() + 1);
                //Si la mémoire physique n'est pas entièrement pleine
                if (!MemoirePleine())
                {
                    //**********************************************************************************
                    // => la memoire n'est pas pleine
                   
                    arr[1] = " et la mémoire n’est pas pleine :" + "\n" + "-	la page" + " " + pg + " " + " est chargée dans la première case vide." + "\n" + "-	Incrémentation de sa fréquence ";
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
                    //*******************************************************************************
                    // la mémoire est  pleine.
                    //******************************************************************************
                    pageCourante.SetNumeroCase(PageAReplacer());
                    //Mettre à jour le numéro de case de la page -dans la table de page-
                    TablePage.ListeTB[pageCourante.numeroPage].NumeroCase = TablePage.ListeTB[PageAReplacer()].NumeroCase;
                    pageAR = PageAReplacer();

                    //specifier le numero de page a remplacer et le rajouter dans la chaine retourner en sortie 
                    String pr = Convert.ToString(PageAReplacer());           
                    arr[1] = "et la mémoire est  pleine" + "\n" + "- Incrémentation de la fréquence de la page " + pg + "\n" + "Remplacement de la page selon LFU:" + "\n" + "- La page en mémoire physique qui correspond a la fréquence la plus petite est remplacée par " + pg+ "\n"+ "- S'il existe deux ou plusieurs pages contenant des fréquences égales. La page a remplacer est choisi parmi ces pages par méthode FIFO(la page qui a été entrée en premier)";

                    TablePage.ListeTB[pageAR].NumeroCase = -1;
                    //TablePage.ListeTB[pageAR].Compteur = 0;
                    RemplacerDansMemoire(pageCourante, TablePage.ListeTB[PageAReplacer()].NumeroCase);
                }
            }
            else
            {
                //**************************************************************************************
                // p = "present" => la page est presente
               
                String pag = Convert.ToString(pageCourante.GetNumeroPage());
                arr[0] = "1. La page" + " " + pag + " " + "existe déjà en mémoire "+"\n"+ "-  Incrémentation de la fréquence de la page ";
             
                
                //**************************************************************************************
            }
            string result = arr[0] + " " + arr[1];
            return result;
        }
    }
}