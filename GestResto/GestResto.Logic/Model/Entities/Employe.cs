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
    /// Classe contenant toutes les informations d'un employ�
    /// </summary>
    public class Employe : INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region Variables appartenant � la classe Employe

        private string _nom;
        private string _prenom;
        private string _noEmploye;

        public virtual int? IdEmploye { get; set; }
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
                _nom = value;
                RaisePropertyChanged();
                RaisePropertyChanged("NomComplet");
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
                _prenom = value;
                RaisePropertyChanged();
                RaisePropertyChanged("NomComplet");
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
                _noEmploye = value;
                RaisePropertyChanged();
                RaisePropertyChanged("NomComplet");
            }
        }
        public virtual string MotDePasse { get; set; }
        public virtual string Adresse { get; set; }
        public virtual string Ville { get; set; }
        public virtual string CodePostal { get; set; }
        public virtual string NAS { get; set; }
        public virtual float Salaire { get; set; }
        public virtual string Telephone { get; set; }
        public virtual bool EstActif { get; set; }
        public virtual TypeEmploye TypeEmployes { get; set; }
        public virtual List<Commande> ListeCommandes { get; set; }

        // Cl�s permettant de crypter et de d�crypter les chaines qu'on veut
        private static readonly string PasswordHash = "P@@Sw0rd";
        private static readonly string SaltKey = "S@LT&KEY";
        private static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        #endregion

        #region Propriet� changed

        public virtual event PropertyChangedEventHandler PropertyChanged;

        public virtual PropertyChangedEventHandler PropertyChangedHandler
        {
            get { return PropertyChanged; }
        }

        public virtual event PropertyChangingEventHandler PropertyChanging;

        public virtual PropertyChangingEventHandler PropertyChangingHandler
        {
            get { return PropertyChanging; }
        }


        public virtual void RaisePropertyChanging([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanging;
            if (handler != null)
            {
                handler(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        public virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Tous les constructeurs de la classe Employes
        /// <summary>
        /// Constructeur par d�faut de la classe Employ�. Elle initialise toutes les variables pour une valeur null ou vide.
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
        /// Constructeur param�tr� de la classe employ�
        /// </summary>
        /// <param name="pIdEmploye">id de l'employ�</param>
        /// <param name="pNom">Nom de l'employ�</param>
        /// <param name="pPrenom">Prenom de l'employ�</param>
        /// <param name="pNoEmploye">Numero de l'employ�</param>
        /// <param name="pMotPasse">Mot de passe de l'employ�</param>
        /// <param name="pAdresse">Adresse de l'employ�</param>
        /// <param name="pVille">Ville de l'employ�</param>
        /// <param name="pCodePostal">Code postal de l'employ�</param>
        /// <param name="pNAS">NAS de l'employ�</param>
        /// <param name="pSalaire">Salaire de l'employ�</param>
        /// <param name="pTelephone">Telephone de l'employ�</param>
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
        /// <param name="pNom">Nom de l'employ�</param>
        /// <param name="pPrenom">Prenom de l'employ�</param>
        /// <param name="pNoEmploye">Numero de l'employ�</param>
        /// <param name="pMotPasse">Mot de passe de l'employ�</param>
        /// <param name="pAdresse">Adresse de l'employ�</param>
        /// <param name="pVille">Ville de l'employ�</param>
        /// <param name="pCodePostal">Code postal de l'employ�</param>
        /// <param name="pNAS">NAS de l'employ�</param>
        /// <param name="pSalaire">Salaire de l'employ�</param>
        /// <param name="pTelephone">Telephone de l'employ�</param>
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
         Tout ce qui permet d'encrypter et de d�crypter est sur le site de microsoft
         */


        /// <summary>
        /// Fonction permettant d'encrypter le mot de passe de l'employe
        /// </summary>
        /// <param name="plainText">Chaine de texte � encrypter</param>
        /// <returns>Chaine crypt�</returns>
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
        /// Fonction qui permet de d�crypter
        /// </summary>
        /// <param name="encryptedText">Chaine crypt�</param>
        /// <returns>Chaine d�crypt�</returns>
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

        #region Fonctions compl�mentaires
        /// <summary>
        /// Fonction qui red�finie la fonction ToString pour permettre la concat�nation du nom et du prenom
        /// </summary>
        /// <returns>Retourne le r�sultat de la concat�nation</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            return builder.Append(Nom).Append(", ").Append(Prenom).Append(" - ").Append(NoEmploye).ToString();
        }

        /// <summary>
        /// Fonction permettant de d�finir la chaine qui sera affich� sur le bouton de l'employ�
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