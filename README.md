 Développement d’une WEB API (.NET ou .NET Core) d’une gestion de cartes de membres d'une
boîte de nuit.
Features :
• Création d'un membre :
	o Enregistrement par carte de membre  et carte d'identité.
	o Lors de la création, vérifier si la personne est éligible.
	
• Mettre à jour les informations d'un membre.
• Capacité de blacklister un membre sur une durée limitée
• Capacité de voir si le membre a été blacklisté.

Contraintes Business :
• La création d'un membre nécessite :
	o une adresse email OU un numéro de téléphone
	o Une carte d'identité VALIDE.
• Une carte de membre est seulement constituée d'un identifiant unique (qui provient, par ex, d'un
QRCode).

• La personne doit avoir > 18 ans.

• Une carte d'identité est constituée d'informations suivantes :
	o Nom
	o Prénom
	o Date de naissance
	o Numéro de registre national au format xxx.xx.xx-xxx-xx
	o Date de validité
	o Date d'expiration
	o Numéro de carte
• Une carte d'identité est unique dans le système.

• Un membre peut s'enregistrer plusieurs fois avec des cartes de membres différentes.
	o Perte de la carte de membre
	o Carte d'identité expirée ou renouvelée.
	

Contraintes techniques :
• WEB API REST (JSON)
• Architecture la plus maintenable possible.
• EF6 ou EF Core ou NHibernate
• Avoir la possibilité d’utiliser une base de données en mémoire et SQLServer (via LocalDB ou
SQLExpress) sans trop modifier le code.
• Faire quelques tests unitaires seulement si nécessaire.
• La solution doit compiler.
• L’API doit répondre via des requêtes POSTMAN
• Github.![image](https://github.com/AymenAbdallah2/NightClubWebApplication/assets/169261708/7900996f-c696-476d-9a04-cfe13c73aa26)
