using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestResto.Logic.Model.Args;
using GestResto.Logic.Model.Entities;
using GestResto.Logic.Services.Definitions;
using GestResto.Logic.Services.Helpers;
using NHibernate;
using NHibernate.Linq;

namespace GestResto.Logic.Services.NHibernate
{
    public class NHibernateEmployeService : IEmployeService
    {
        // Définition des variables de session
        private ISession session = NHibernateConnexion.OpenSession();
        private ISession sessionLazy = NHibernateConnexion.OpenSession();

        #region IEmployeService Membres

        /// <summary>
        /// Focntion permettant de créer un employé
        /// </summary>
        /// <param name="employe">Prend un employé en paramètre</param>
        public void Create(Employe employe)
        {
            // On réinitialise la session
            session = NHibernateConnexion.OpenSession();

            // On effectue la transaction
            using (var transaction = session.BeginTransaction())
            {
                session.Save(employe);
                transaction.Commit();
            }
            session.Close();
        }

        /// <summary>
        /// Fonction permettant de charger la liste des employés
        /// </summary>
        /// <returns>Retourne la liste de tous les employés</returns>
        public IList<Employe> RetriveAll()
        {
            // On redéfinie la variable de session
            sessionLazy = NHibernateConnexion.OpenSession();

            // On définie la requète à la base de données
            var result = from c in sessionLazy.Query<Employe>()
                            orderby c.EstActif descending, 
                            c.Nom ascending, c.Prenom ascending
                            select c;

            // On se définie une liste temporaire
            IList<Employe> listeTemp = result.ToList();

            // On boucle dans tous nos résultat pour décrypter le mot de passe
            // pour ne pas boucler dans la vue pour les mots de passe
            foreach (var employe in listeTemp)
            {
                employe.MotDePasse = Employe.Decrypt(employe.MotDePasse);
            }

            // On retourne la liste
            return listeTemp;
        }

        /// <summary>
        /// Fonction permettant de sélectionner un employé
        /// </summary>
        /// <param name="args">Liste des arguments permettant de sélectionner un employé</param>
        /// <returns>Retourne l'employé recherché</returns>
        public Employe Retrive(RetrieveEmployeArgs args)
        {
            // On redéfinie la variable de session
            session = NHibernateConnexion.OpenSession();

            // On se crée un employé temporaire
            Employe employeTemp;

            // On test si on vient avec un mot de passe et un numéro d'employé
            // si c'est le cas c'est qu'on tente de se connecter
            if (args.MDP != null && args.NoEmploye != null)
            {
                // On effectue la requête avec les paramètres
                var result = from c in session.Query<Employe>()
                             where c.MotDePasse == args.MDP &&
                             c.NoEmploye == args.NoEmploye
                             select c;

                // On enregistre le résultat dans notre employé temporaire
                employeTemp = result.FirstOrDefault();
            }
            else
            {
                // On effectue la requête avec seulement un ID puisqu'on n'est pas ici pour s'authentifier
                var result = from c in session.Query<Employe>()
                             where c.IdEmploye == args.IIdEmploye
                             select c;

                // On enregistre dans notre employé temporaire
                employeTemp = result.FirstOrDefault();
            }

            // On ferme la session et on retourne l'employé sélectionné
            session.Close();
            return employeTemp;
        }

        /// <summary>
        /// Fonction permettant de modifier un employé existant
        /// </summary>
        /// <param name="employe">Employé qu'on souhaite modifier</param>
        public void Update(Employe employe)
        {
            // On enregistre le mot de passe non encrypté pour éviter d'encrypter 2 fois le même mot de passe
            string AncienMDPNonEncrypte = employe.MotDePasse;

            // On encrypte le mot de passe
            employe.MotDePasse = Employe.Encrypt(employe.MotDePasse);

            // On réinitialise la variable de session
            session = NHibernateConnexion.OpenSession();

            // On effectue la transaction
            using (var transaction = session.BeginTransaction())
            {
                session.Update(employe);
                transaction.Commit();
            }

            // On ferme la session
            session.Close();

            // On redéfinie le mot de passe à l'objet par le mot de passe non encrypté
            employe.MotDePasse = AncienMDPNonEncrypte;
        }

        #endregion
    }
}
