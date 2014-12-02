�tapes pour utiliser GestResto
1. Cr�er une base de donn�es nomm�e : gestresto
	1.1. Cette �tape peut �tre accomplie gr�ce au logiciel XAMP et phpmyadmin.
	1.2. Vous pouvez changer le nom de la base de donn�es, mais vous devez modifier 3 fichiers (Facultatif et avanc�).
		1.2.1. La premi�re ligne dans scriptStructure, vous pouvez voir le mot gestresto, changez le par le nom de votre base de donn�es.
		1.2.2. La premi�re ligne dans scriptDonnes, vous pouvez voir le mot gestresto, changez le par le nom de votre base de donn�es.
		1.2.3. Vous devez chercher dans le fichier GestResto.UI.exe.config environ � la ligne 26, une balise property en dessous de celle-ci il y a un Database=gestresto, vous devez changer le nom pour le nom de votre base de donn�es, de plus vous pouvez utiliser un utilisateur pr�alablement ajout� � votre base de donn�es.
2. Allez dans l'onglet de PHPmyadmin pour importer un script SQL.
	2.1. Importez le script SQL fournit avec ce fichier nomm� : scriptStructure.sql
	2.2. La base de donn�es est maintenant cr��e. Il faut laisser XAMP ouvert pour donner l'acc�s � GestResto.
	2.3. (Facultatif) Vous pouvez ajouter les donn�es de d�monstration en faisant la m�me op�ration que la 2.1 avec un autre fichier nomm� : scriptDonnees.sql
3. Ouvrir GestResto.
	3.1. Vous pouvez vous connecter avec l'administrateur d�j� cr��. Son num�ro de compte et 11 et son mot de passe est 11.
	3.2. Nous vous conseillons fortement de changer ce mot de passe dans les options d'administrations et ensuite dans la gestion des employ�s.
	3.3. Vous devez aussi modifier les informations de votre restaurant pour que la facture soit bien cr��e. (La facture n'est pas encore impl�ment�.)
4. Pour commencer votre menu, nous vous conseillons de commencer par la cr�ation des cat�gories, ensuite par les formats et � la toute fin cr�er les items et les employ�s.
5. (Optionnel) Si vous voulez h�berger votre base de donn�es ailleurs, vous pouvez changer le fichier de configuration nomm� : GestResto.UI.exe.config. Vous devez red�marrer l'application.
6. Nous offrons d�j� des donn�es qui permettent la d�monstraton de notre logiciel � son plein potentiel, donc vous pouvez vous authentifi� en tant que serveur avec son num�ro : 33 et son mot de passe : 33.
7. En �tant connect� comme serveur, vous pouvez cr�er une commande, cr�er des clients et leurs assigner des items pour ensuite cr�er une facture. Pour plus d'informations, consult� le document Guide d'utilisation.