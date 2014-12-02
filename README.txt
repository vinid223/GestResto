Étapes pour utiliser GestResto
1. Créer une base de données nommée : gestresto
	1.1. Cette étape peut être accomplie grâce au logiciel XAMP et phpmyadmin.
	1.2. Vous pouvez changer le nom de la base de données, mais vous devez modifier 3 fichiers (Facultatif et avancé).
		1.2.1. La première ligne dans scriptStructure, vous pouvez voir le mot gestresto, changez le par le nom de votre base de données.
		1.2.2. La première ligne dans scriptDonnes, vous pouvez voir le mot gestresto, changez le par le nom de votre base de données.
		1.2.3. Vous devez chercher dans le fichier GestResto.UI.exe.config environ à la ligne 26, une balise property en dessous de celle-ci il y a un Database=gestresto, vous devez changer le nom pour le nom de votre base de données, de plus vous pouvez utiliser un utilisateur préalablement ajouté à votre base de données.
2. Allez dans l'onglet de PHPmyadmin pour importer un script SQL.
	2.1. Importez le script SQL fournit avec ce fichier nommé : scriptStructure.sql
	2.2. La base de données est maintenant créée. Il faut laisser XAMP ouvert pour donner l'accès à GestResto.
	2.3. (Facultatif) Vous pouvez ajouter les données de démonstration en faisant la même opération que la 2.1 avec un autre fichier nommé : scriptDonnees.sql
3. Ouvrir GestResto.
	3.1. Vous pouvez vous connecter avec l'administrateur déjà créé. Son numéro de compte et 11 et son mot de passe est 11.
	3.2. Nous vous conseillons fortement de changer ce mot de passe dans les options d'administrations et ensuite dans la gestion des employés.
	3.3. Vous devez aussi modifier les informations de votre restaurant pour que la facture soit bien créée. (La facture n'est pas encore implémenté.)
4. Pour commencer votre menu, nous vous conseillons de commencer par la création des catégories, ensuite par les formats et à la toute fin créer les items et les employés.
5. (Optionnel) Si vous voulez héberger votre base de données ailleurs, vous pouvez changer le fichier de configuration nommé : GestResto.UI.exe.config. Vous devez redémarrer l'application.
6. Nous offrons déjà des données qui permettent la démonstraton de notre logiciel à son plein potentiel, donc vous pouvez vous authentifié en tant que serveur avec son numéro : 33 et son mot de passe : 33.
7. En étant connecté comme serveur, vous pouvez créer une commande, créer des clients et leurs assigner des items pour ensuite créer une facture. Pour plus d'informations, consulté le document Guide d'utilisation.