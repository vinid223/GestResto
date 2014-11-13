//
//
//  
//
//  Projet : GestResto
//  Nom du fichier : Employe.cs
//  Date : 2014-09-25
//  Auteurs : Tommy Demers, Vincent Desrosiers et Simon Turcotte
//
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Model.Entities
{
    /// <summary>
    /// Classe contenant toutes les informations d'un employé
    /// </summary>
    public class Employe : BaseEntity
    {
        #region Variables appartenant à la classe Employe

        public virtual int? IdEmploye { get; set; }
        private string _nom;
        private string _prenom;
        private string _noEmploye;
        private string _motDePasse;
        private string _adresse;
        private string _ville;
        private string _codePostal;
        private string _nas;
        private float _salaire;
        private string _telephone;
        private bool _estActif;
        private TypeEmploye _typeEmployes;
        private List<Commande> _listeCommandes;
        private bool _estModifie;

        #endregion

        #region Propriété

        public virtual string Nom
        {
            get
            {
                return _nom;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("NomComplet");
                RaisePropertyChanging("EstModifie");
                _nom = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("NomComplet");
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual string Prenom
        {
            get
            {
                return _prenom;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("NomComplet");
                RaisePropertyChanging("EstModifie");
                _prenom = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("NomComplet");
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual string NoEmploye
        {
            get
            {
                return _noEmploye;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("NomComplet");
                RaisePropertyChanging("EstModifie");
                _noEmploye = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("NomComplet");
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual string MotDePasse
        {
            get
            {
                return _motDePasse;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _motDePasse = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }

        public virtual string Adresse
        {
            get
            {
                return _adresse;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _adresse = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual string Ville
        {
            get
            {
                return _ville;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _ville = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual string CodePostal
        {
            get
            {
                return _codePostal;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _codePostal = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual string NAS
        {
            get
            {
                return _nas;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _nas = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual float Salaire
        {
            get
            {
                return _salaire;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _salaire = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual string Telephone
        {
            get
            {
                return _telephone;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _telephone = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual bool EstActif
        {
            get
            {
                return _estActif;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _estActif = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual TypeEmploye TypeEmployes
        {
            get
            {
                return _typeEmployes;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _typeEmployes = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual List<Commande> ListeCommandes
        {
            get
            {
                return _listeCommandes;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _listeCommandes = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual bool EstModifie
        {
            get
            {
                return _estModifie;
            }
            set
            {
                RaisePropertyChanging();
                _estModifie = value;
                RaisePropertyChanged();
            }
        }


        // Clés permettant de crypter et de décrypter les chaines qu'on veut
        private static readonly string PasswordHash = "P@@Sw0rd";
        private static readonly string SaltKey = "S@LT&KEY";
        private static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        #endregion

        #region Tous les constructeurs de la classe Employes
        /// <summary>
        /// Constructeur par défaut de la classe Employé. Elle initialise toutes les variables pour une valeur null ou vide.
        /// </summary>
        public Employe()
        {
            IdEmploye = null;
            Nom = "";
            Prenom = "";
            NoEmploye = "";
            MotDePasse = "";
            Adresse = "";
            Ville = "";
            CodePostal = "";
            NAS = "";
            Salaire = 0;
            Telephone = "";
            EstActif = false;
            TypeEmployes = null;
            ListeCommandes = new List<Commande>();
        }

        /// <summary>
        /// Constructeur paramétré de la classe employé
        /// </summary>
        /// <param name="pIdEmploye">id de l'employé</param>
        /// <param name="pNom">Nom de l'employé</param>
        /// <param name="pPrenom">Prenom de l'employé</param>
        /// <param name="pNoEmploye">Numero de l'employé</param>
        /// <param name="pMotPasse">Mot de passe de l'employé</param>
        /// <param name="pAdresse">Adresse de l'employé</param>
        /// <param name="pVille">Ville de l'employé</param>
        /// <param name="pCodePostal">Code postal de l'employé</param>
        /// <param name="pNAS">NAS de l'employé</param>
        /// <param name="pSalaire">Salaire de l'employé</param>
        /// <param name="pTelephone">Telephone de l'employé</param>
        public Employe(int pIdEmploye, string pNom, string pPrenom, string pNoEmploye, string pMotPasse, string pAdresse, string pVille, string pCodePostal, string pNAS, float pSalaire, string pTelephone, bool pEstActif, TypeEmploye pType)
        {
            IdEmploye = pIdEmploye;
            Nom = pNom;
            Prenom = pPrenom;
            NoEmploye = pNoEmploye;
            MotDePasse = pMotPasse;
            Adresse = pAdresse;
            Ville = pVille;
            CodePostal = pCodePostal;
            NAS = pNAS;
            Salaire = pSalaire;
            Telephone = pTelephone;
            EstActif = pEstActif;
            TypeEmployes = pType;
        }

        /// <summary>
        /// Constructeur d'employer sans id d'employer
        /// </summary>
        /// <param name="pNom">Nom de l'employé</param>
        /// <param name="pPrenom">Prenom de l'employé</param>
        /// <param name="pNoEmploye">Numero de l'employé</param>
        /// <param name="pMotPasse">Mot de passe de l'employé</param>
        /// <param name="pAdresse">Adresse de l'employé</param>
        /// <param name="pVille">Ville de l'employé</param>
        /// <param name="pCodePostal">Code postal de l'employé</param>
        /// <param name="pNAS">NAS de l'employé</param>
        /// <param name="pSalaire">Salaire de l'employé</param>
        /// <param name="pTelephone">Telephone de l'employé</param>
        public Employe(string pNom, string pPrenom, string pNoEmploye, string pMotPasse, string pAdresse, string pVille, string pCodePostal, string pNAS, float pSalaire, string pTelephone, bool pEstActif, TypeEmploye pType)
        {
            Nom = pNom;
            Prenom = pPrenom;
            NoEmploye = pNoEmploye;
            MotDePasse = pMotPasse;
            Adresse = pAdresse;
            Ville = pVille;
            CodePostal = pCodePostal;
            NAS = pNAS;
            Salaire = pSalaire;
            Telephone = pTelephone;
            EstActif = pEstActif;
            TypeEmployes = pType;
        }

        #region Encryption
        /*
         Source de l'encryption : https://social.msdn.microsoft.com/Forums/vstudio/en-US/d6a2836a-d587-4068-8630-94f4fb2a2aeb/encrypt-and-decrypt-a-string-in-c
         Tout ce qui permet d'encrypter et de décrypter est sur le site de microsoft
         */


        /// <summary>
        /// Fonction permettant d'encrypter le mot de passe de l'employe
        /// </summary>
        /// <param name="plainText">Chaine de texte à encrypter</param>
        /// <returns>Chaine crypté</returns>
        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        /// <summary>
        /// Fonction qui permet de décrypter
        /// </summary>
        /// <param name="encryptedText">Chaine crypté</param>
        /// <returns>Chaine décrypté</returns>
        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        #endregion

        #endregion

        #region Fonctions complémentaires
        /// <summary>
        /// Fonction qui redéfinie la fonction ToString pour permettre la concaténation du nom et du prenom
        /// </summary>
        /// <returns>Retourne le résultat de la concaténation</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            return builder.Append(Nom).Append(", ").Append(Prenom).Append(" - ").Append(NoEmploye).ToString();
        }

        /// <summary>
        /// Fonction permettant de définir la chaine qui sera affiché sur le bouton de l'employé
        /// </summary>
        public virtual string NomComplet
        {
            get
            {
                return ToString();
            }
        }

        #endregion
    }
}