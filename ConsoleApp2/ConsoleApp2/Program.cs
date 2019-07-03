using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creation de 8 pages
            PageCase p0 = new PageCase(0);
            PageCase p1 = new PageCase(1);
            PageCase p2 = new PageCase(2);
            PageCase p3 = new PageCase(3);
            PageCase p4 = new PageCase(4);
            PageCase p5 = new PageCase(5);
            PageCase p6 = new PageCase(6);
            PageCase p7 = new PageCase(7);


            //***** LFU  *****//


            //Creation d'un systeme LFU
            /* SystemLFU lfu = new SystemLFU(1024, 128, 512); //768 étant la taille de mémoire physique et 256 la taille de la case/page mémoire

            //Charger la liste de l'utilisateur
            lfu.ajouterAListe(p2);
            lfu.ajouterAListe(p0);
            lfu.ajouterAListe(p3);
            lfu.ajouterAListe(p2);
            lfu.ajouterAListe(p0);
            lfu.ajouterAListe(p5);
            lfu.ajouterAListe(p7);
            lfu.ajouterAListe(p1);
            lfu.ajouterAListe(p3);
            lfu.ajouterAListe(p1);
            lfu.ajouterAListe(p6);

            //Simuler la gestion avec algorithme LFU
            lfu.deroulerAlgorithme(); */

            //-**** Aging  *****-//


            //Creation d'un systeme Aging
            // SystemeAging aging = new SystemeAging(64, 32, 16); //768 étant la taille de mémoire physique et 256 la taille de la case/page mémoire

            //SystemeLFU lfu = new SystemeLFU(64, 32, 16);
            //Charger la liste de l'utilisateur
            /*aging.AjouterAListe(p2);
              aging.AjouterAListe(p0);
              aging.AjouterAListe(p3);
              aging.AjouterAListe(p2);*/

            /*lfu.AjouterAListe(p2);
            lfu.AjouterAListe(p0);
            lfu.AjouterAListe(p3);
            lfu.AjouterAListe(p2);*/
            SystemeAging aging = new SystemeAging(32,12,4);
            aging.AjouterAListe(p0);
            aging.AjouterAListe(p0);
            aging.AjouterAListe(p2);
            int i = 0;
            Console.WriteLine("" + aging.ListeUtilisateur.Count);
            //Simuler la gestion avec algorithme Aging
            while (i < aging.ListeUtilisateur.Count) {
                //Console.WriteLine("heeeere");
                string str = aging.DeroulerAlgorithme();
                Console.WriteLine("" + str);
                Console.ReadLine();
                i++;
            }
            
        }
    }
}

