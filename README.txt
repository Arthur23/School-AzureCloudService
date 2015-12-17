Bonjour,

Pour démarrer le projet il faudra s'assurer de quelques petits détails :
Avoir VS 2015 d'installé, et en cas de manque de références / librairies :
Vérifier les packages nugget, ou références (Azure / File / IO ...)

Au démarrage de la solution, deux projets devraient se lancer :
	- SmallBoxCloud
	- SmallBox_Client
Il est possible de vérifier par clique droit sur la solution --> Définir les projets au démarrage...

Au lancement de l'application :
Le constructeur de la classe du service se charge justement de construire le contexte
ou la structure du Cloud Storage en voici un modele

smallboxcontainer	/--Archives
							-- Readme.txt
					---Backups
							-- Readme.txt
					---Documents
							-- Readme.txt
					---Images
							-- Readme.txt
					---Vidéos
							-- Readme.txt

Par conséquent, avant d'utiliser le client, il vaut mieux attendre sagement que le worker ait fait sont boulot
pour passer la main au client, et particulièrement lors d'un premier déploiement.
On peut savoir que le worker a finit sa tâche lorsque la page internet s'affiche dans notre navigateur configuré
dans visual studio.

Grace au client, nous pouvons gérer certaines choses, telles qu'empêcher d'uploader à certains endroits,
ou de "déléguer" la tâche et la charge au client qui fait lui même la compression des dossiers avant l'envoi,
ou qui calcul les chaines de caractères à envoyer pour interagir avec les methodes du webservices prêtes à accueillir
des appels selon certains paramètres.

L'application a été faite pour que les fichiers et dossiers soient automatiquements installés au démarrage,
et pour enregistrer les fichiers localement à destination du DESKTOP de la machine, faite pour qu'il n'y ait 
normalement rien à faire pour qu'elle se lance et qu'elle fonctionne parfaitement.

Attention Toutefois à des bugs qui peuvent arriver pour les uploads / download, lorsque les fichiers ou dossiers 
existent déjà, toutes ces exceptions ne sont pas gérées faute de temps.
Il est tout à fait jouable de lancer l'application et de réaliser chacune des tâche au moins une fois sans bugs (Testé)

Les morceaux de codes principaux sont entourés de #régions pour faciliter la recherche (et son nommées selon le barème à peu de choses près)
Le Compute Emulator UI permet lui d'avoir une trace en console de ce qu'il se passe chez le worker (brièvement)
Outre le client développé, il est égallement possible d'avoir accès à certaines de ces fonctions par le biais du WCFTestClient généré avec VS

Si l'application bug au démarrage, supprimer le container et vérifier si des fichiers ne sont pas restés sur le bureau.