Information : La connexion à la base de données est programmé pour se connecter au serveur local avec un 
usager root sans mot de passe. Il est possible de modifier ces configurations dans le fichier App.confg
dans le répertoire GestResto.UI

Étapes pour utiliser GestResto
1. Créer une base de données nommée : 5a5_a14_gestresto
	1.1. Cette étape peut être accomplie grâce au logiciel XAMP et phpmyadmin.
2. Allez dans l'onglet de PHPmyadmin pour importer un script SQL.
	2.1. Importez le script SQL fournit avec ce fichier nommé : scriptMySQLBeta.sql
	2.2. La base de données est maintenant créée. Il faut laisser XAMP ouvert pour donner l'accès à GestResto.
3. Ouvrir GestResto.
	3.1. Vous pouvez vous connecter avec l'administrateur déjà créé. Son numéro de compte et 11 et son mot de passe est 11.
	3.2. Nous vous conseillons fortement de changer ce mot de passe dans les options d'administrations et ensuite dans la gestion des employés.
	3.3. Vous devez aussi modifier les informations de votre restaurant pour que la facture soit bien créée. (La facture n'est pas encore implémenté.)
4. Pour commencer votre menu, nous vous conseillons de commencer par la création des catégories, ensuite par les formats et à la toute fin créer les items et les employés.
