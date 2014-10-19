//
//
//  
//
//  Projet : GestResto
//  Nom du fichier : Format.cs
//  Date : 2014-09-25
//  Auteurs : Tommy Demers, Vincent Desrosiers et Simon Turcotte
//
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Model.Entities
{
    /// <summary>
    /// La classe format est g�n�ralement associ� � un item,
    /// un format permet � l'utilisateur de savoir le prix de l'item,
    /// il peut avoir 1 format minimum et plusieurs qui sera choisi lors de la facture.
    /// </summary>
    public class Format
    {
        #region Variables de la classe

        public virtual int? IdFormat { get; set; }
        public virtual string Nom { get; set; }
        public virtual string Libelle { get; set; }
        public virtual bool EstActif { get; set; }

        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur par d�faut qui met le id null et les autres champs vides.
        /// </summary>
        public Format()
        {
            IdFormat = null;
            Nom = "";
            Libelle = "";
            EstActif = false;
        }

        /// <summary>
        /// Constructeur param�tr� qui met tous les param�tres dans les attributs de l'objet.
        /// </summary>
        /// <param name="pIdFormat">Le id du format en param�tre</param>
        /// <param name="pNom">Le nom du format en param�tre.</param>
        /// <param name="pLibele">Le libel� en param�tre.</param>
        /// <param name="pEstActif">Statut du format par true ou false pour actif ou non</param>
        public Format(int pIdFormat, string pNom, string pLibelle, bool pEstActif)
        {
            IdFormat = pIdFormat;
            Nom = pNom;
            Libelle = pLibelle;
            EstActif = pEstActif;
        }

        /// <summary>
        /// Constructeur param�tr� qui met tous les param�tres dans les attributs de l'objet qui permet d'ins�r� dans la BD.
        /// </summary>
        /// <param name="pNom">Le nom du format en param�tre.</param>
        /// <param name="pLibele">Le libel� en param�tre.</param>
        /// <param name="pEstActif">Statut du format par true ou false pour actif ou non</param>
        public Format(string pNom, string pLibelle, bool pEstActif)
        {
            Nom = pNom;
            Libelle = pLibelle;
            EstActif = pEstActif;
        }

        #endregion


        #region Red�finition de fonctions de bases
        /// <summary>
        /// Permet de comparer deux cat�gories.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Format p = obj as Format;

            if (p == null)
            {
                return false;
            }

            return this.IdFormat == p.IdFormat;
        }



        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            return builder.Append(Nom).ToString();
        }
        #endregion
    }
}