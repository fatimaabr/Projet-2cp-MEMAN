
using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    [Serializable]
    public class SystemeLRU : PageReplacmment
    {
        //List<PageCase> listeLRU = new List<PageCase>();
        public List<PageCase> ListeLRU { get; set; }

        public SystemeLRU(int tailleMemoire, int tailleCase) : base(tailleMemoire, tailleCase)
        {
            ListeLRU = new List<PageCase>();
        }

        public override int PageAReplacer()
        {
            //Récupérer l'emplacement en mémoire physique de la page en queue de liste -la moins récemment utilisée-
            int i = ListeLRU[ListeLRU.Count - 1].GetNumeroCase();
            return i;
            throw new NotImplementedException();
        }

        public override string DeroulerAlgorithme()
        {
            //Tant que la file entrée par l'utilisateur n'est pas terminée
            //while (base.GetTailleListeUtilisateur() != 0)

            //Récupérer la tête de la liste entrée par l'utilisateur
            PageCase pageCourante = GetListei(0);
            //Liberer la tete de la liste
            SuppDeListe(0);
            string[] arr = new string[4];
            if (PageExiste(pageCourante.GetNumeroPage()))
            {
                //*******************************************************************************************
                // p = "present" => la page existe deja 
                String pag = Convert.ToString(pageCourante.GetNumeroPage());
                arr[0] = "1. La page" + " " + pag + " " + "existe déjà en mémoire "+"\n"+ "- elle est supprimée de la liste LRU et est insérée en tête de cette dernière. ";

            
             
                //********************************************************************************************
                /*Supprimer la page de la liste LRU*/
                //Récupérer son indice dans la liste
                ListeLRU.Remove(pageCourante);
                //Inserer la page courante en tête de listei
                ListeLRU.Insert(0, pageCourante);
            }
            else
            {
                // la page n'existe pas 
                String pg = Convert.ToString(pageCourante.GetNumeroPage());
                arr[0] = "1. La page" + " " + pg + " " + "n’existe pas en mémoire (défaut de page ) ";
               

                //Inserer la page courante en tête de liste
                ListeLRU.Insert(0, pageCourante);
                SetDefautPages(GetDefautPage() + 1);
                //Si la mémoire physique n'est pas entièrement remplie
                if (!MemoirePleine())
                {
                    //**********************************************************************************
                    // n = "not full" => la memoire n'est pas pleine
                    arr[1] = " et la mémoire n’est pas pleine :" + "\n" + "-	la page" + " " + pg + " " + " est chargée dans la première case vide.";
            
                    //*********************************************************************************
                    //Charger la page à la première case vide
                    pageCourante.SetNumeroCase(PremiereCaseLibre());
                    //ajouterAMemoire(pageCourante);
                    RemplacerDansMemoire(pageCourante, PremiereCaseLibre());
                    //Décrémenter le nombre de cases libres en mémoire physique
                    DecCasesLibre();
                }
                //Si la mémoire physique est entièrement remplie
                else
                {
                    //*******************************************************************************
                    // => la memoire est pleine                   
                    //******************************************************************************
                    //utiliser l'algorithme de remplacement LRU
                    pageCourante.SetNumeroCase(PageAReplacer());
                    RemplacerDansMemoire(pageCourante, PageAReplacer());
                    
                    String pr = Convert.ToString(PageAReplacer());
                    arr[1] = "et la mémoire est  pleine" + "\n" + "Remplacement de la page selon LRU:" + "\n" + "-	Récupérer l’emplacement en mémoire physique(numéro de case ) de la page en queue de la liste LRU(la moins récemment utilisée ) =" + pr;

                    //Supprimer la page de la liste
                    ListeLRU.RemoveAt(ListeLRU.Count - 1);

                }
            }
            string result = arr[0] + " " + arr[1];
            return result;

        }
    }
}