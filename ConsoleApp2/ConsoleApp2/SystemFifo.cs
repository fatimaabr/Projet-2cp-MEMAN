
using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    [Serializable]
    public class SystemFifo : PageReplacmment
    {
        private int time;

        public Queue<PageCase> FileFIFO { get; set; }
        public SystemFifo(int tailleMemoire, int tailleCase) : base(tailleMemoire, tailleCase)
        {
            FileFIFO = new Queue<PageCase>();
        }

        //Implémentation des méthodes abstraites
        public override int PageAReplacer()
        {
         
            //Defiler la file (Récupérer la page qui a été entrée en premier) et renvoyer son emplacement en mémoire physique en sortie de la méthode
            return FileFIFO.Peek().GetNumeroCase();
            throw new NotImplementedException();
        }

        public override string DeroulerAlgorithme()
        {
            //temps d'attente entre les instructions de l'algorithme

            //Tant que la file entrée par l'utilisateur n'est pas terminée
            //while (base.GetTailleListeUtilisateur() != 0)
            string[] arr = new string [4] ;
            Console.WriteLine(time++);
            //Récupérer la tête de la liste entrée par l'utilisateur
            PageCase pageCourante = GetListei(0);
            //Liberer la tete de la liste
            SuppDeListe(0);
            //Vérifier que la page est déjà en mémoire physique
            if (!PageExiste(pageCourante.GetNumeroPage()))
            {
                //******************************************************************************************
                // la page n'existe pas 
                String pg = Convert.ToString(pageCourante.GetNumeroPage());
                arr[0] = " La page" + " " + pg + " " + "n’existe pas en mémoire (défaut de page ) "; 
                
                //********************************************************************************************/
                SetDefautPages(GetDefautPage() + 1);
                //Enfiler la pageCourante
                FileFIFO.Enqueue(pageCourante);
                //Si la mémoire physique n'est pas entièrement remplie
                if (!MemoirePleine())
                {
                    //********************************************************************************
                    // n = "not full" => la memoire n'est pas pleine
                    arr[1] = " et la mémoire n’est pas pleine :"+ "\n"+ "-	la page"+" "+pg+" "+" est chargée dans la première case vide.";
                    //********************************************************************************
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
                
                    //******************************************************************************
                    // f = "full" => la memoire est pleine                  
                    //******************************************************************************
                    //utiliser l'algorithme de remplacement FIFO
                    pageCourante.SetNumeroCase(PageAReplacer());
                    RemplacerDansMemoire(pageCourante, PageAReplacer());
                    //specifier le numero de page a remplacer et le rajouter dans la chaine retourner en sortie 
                    String pr = Convert.ToString(PageAReplacer());
                    arr[1] = "et la mémoire est  pleine" + "\n" + "Remplacement de la page selon FIFO:" + "\n" + "- Défiler la file(récupérer la page qui a été entrée en premier) =" +pr;
                    
                    //Defiler la page remplacer
                    FileFIFO.Dequeue();
                }
            }
            else
            {
                //**************************************************************************************
                // p = "present" => la page est presente
                String pag = Convert.ToString(pageCourante.GetNumeroPage());
                arr[0] = " La page" + " " + pag + " " + "existe déjà en mémoire ";
                //indiquer le numero de page existante deja et la rajouter dans la chaine 


                //**************************************************************************************
            }
            string result = arr[0]+" "+arr[1];
            return result;
        }
    }
}