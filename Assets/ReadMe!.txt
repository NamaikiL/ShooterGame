Changelog:

+ Ajout de pièces.
+ Ajout du Heal.
+ Ajout de power Up:
    - FireRatePlus: Augment le taux de tire par deux. (30s)
    - DamagePlus: Double les dégats. (30s)
    - Shield: Donne un shield au joueur. (10s)
    - Nuke: Détruit tout les ennemis actuel sur la map.
    - Despawn après 30 secondes sur la map.
+ Ajout drops pour les ennemis:
    - Pièces (100% de chance de drop, avec un nombre min/max de spawn prédéfinit par le MJ).
    - Heal (100% de chance de drop, avec un nombre min/max de spawn prédéfinit par le MJ).
    - PowerUps (Chance de drop prédéfinit par le MJ).
+ Ajout du tir ennemi.
+ Ajout d'un menu.
+ Ajout d'un menu de mort.
+ Le joueur meurt quand il n'a plus de point de vies.
+ Load le dernier niveau ou le joueur était au restart.
+ Ajout des sons fournis:
    Coin pour la récupération de pièces.
    Explosion pour la mort des ennemis/du joueur.
    Shield pour l'activation du shield.
    Tirs1 pour les tirs standard des ennemis/du joueur.
    Vie pour la récupération de vie. |Pas encore fait.
    Action Cartoon Music pour les niveaux.
+ Ajout de conditions de victoire:
    - Lorsque le joueur tue un nombre d'ennemis égale au nombre d'ennemis à tuer choisit par le MJ, il gagne.
    - Lorsque le joueur atteint une wave limite prédéfinit par le MJ, il gagne.
+ Ajout des différents niveaux:
    - Défilement visuels des niveaux pour faire "Avancer" le joueur.
    - 3 Zones différentes: Town, Factory, Base.
    - à chaque fin de niveau, le joueur passe au prochain.
+ Ajout de différents ennemis:
    - LowerClassEnemy: Ennemis basique tirant des balles simples avec des stats raisonnable.
    - HigherClassEnemy: Ennemis supérieur tirant des balles simples avec plus de cannons, faisant de plus gros dégats mais tirant moins vite.
    - Bomber: Ennemis supérieur tirant des missiles.
    - Boss: Ennemi final, tirant balles et missiles autoguidé. | Pas fini
+ Ajout de plusieurs niveaux de vaisseaux joueur:
    - Différent mode de tir avec le niveau 3, appuyez sur A/touchez le bouton afin de changer entre le tir bullet/missile.
    - Achat des différents niveau dans le shop.
+ Ajout du Shop:
    - Vends les améliorations du vaisseau.
    - Vends de l'énergie pour réparer le vaisseau.
    - Vends un boost random.
+ Ajout des animations et de sons.

# Fix des shoot || Les bullets n'activaient pas le OnCollisionEnter/OnTriggerEnter dû à l'utilisation des Physics.IgnoreLayerCollision.   
# Modification du système basique de wave en un système plus complexe:
    - Divisé en trois étapes: Waves, Phase et Spawn.
    - Une wave contient plusieurs phase, qui contient plusieurs ennemis à faire spawn.
    - Une fois que tout les ennemis d'une phase sont bien mort par un joueur/arrive à leur destination, le jeu attend un nombre de secondes donné par le MJ pour spawn la prochaine phase.
    - Une fois que toutes les phases sont passés, la wave est gagné, le joueur attend un certains temps définit par le MJ avant de passé à la prochaine wave.
    - Chaque nouvelle wave contient plus de phase avec deux valeur(Min et max) prédéfinit par le MJ qui s'ajouteront au total de phase à faire. (Exemple: Si la wave 1 contient 3 phase, et que les valeurs min/max contiennent 0/5, alors la prochaine wave aura 3 + un nombre entre 0 et 5-1(car exclu) de phase).
    - Chaque ennemi spawn soit sur des spawn prédéfinit à droite ou à gauche, et à pour but de rejoindre un spawn opposé.
# Fix du déplacement des vaisseaux ennemis | Changement de la fonction Translate avec la fonction MoveTowards dû à un bug non fixable(?).


Assets Externes utilisé:

// Unitypackage

Substance 3D for Unity unitypackage | Adobe Substance 3D
https://assetstore.unity.com/packages/tools/utilities/substance-3d-for-unity-213208

Sci-fi GUI skin unitypackage | 3d.rina
https://assetstore.unity.com/packages/2d/gui/sci-fi-gui-skin-15606


// Models

32MaquisRaider 3D Model | ELVENCITY (Utilisé comme Boss)
https://www.turbosquid.com/fr/3d-models/3d-32maquisraider-1969780#

Baril arc-en-ciel de science-fiction 3D Model| CGGame (Utilisé comme Heal)
https://www.turbosquid.com/fr/3d-models/3d-scifi-rainbow-barrel-model-1758254

Star Sparrow Modular Spaceship 3D Model | Ebalstudios (Utilisé comme vaisseaux ennemis)
https://www.cgtrader.com/free-3d-models/space/spaceship/star-sparrow-modular-spaceship

Sci Fi Barrels 3D Model | jamyzgenius (Utilisé comme powerups)
https://www.cgtrader.com/free-3d-models/exterior/sci-fi-exterior/sci-fi-barrels-free-download

missile 1 | lukass12 (Utilisé comme missile)
https://www.cgtrader.com/free-3d-models/military/rocketry/missile-1


// Sons

Action Cartoon Music audio | Mattia Cupelli (Utilisé pour le menu, donné dans le pack sons)
https://www.youtube.com/watch?v=H84sldKc6aE

Coin audio | Imphenzi (Utilisé pour la récolte de pièces, donné dans le pack sons)
/

Explosion audio | / (Utilisé pour la mort d'un ennemi/du joueur, donné dans le pack sons)
/

Shield audio | / (Utilisé lors de l'activation du shield, donné dans le pack sons)
/

Tirs1 audio | / (Utilisé lors des tirs standard ennemis/joueur, donné dans le pack sons)
/

Vie audio | / (Utilisé quand le joueur récolte un heal, donné dans le pack sons)
/

Kirby and the Rainbow Curse/Paintbrush - Break into! The Junk Factory audio | Nintendo (Utilisé pour la zone Factory)
https://www.youtube.com/watch?v=QHA7Zwdz5HQ

Radiant Historia - The Edge of Green audio | Yoko Shimomura (Utilisé pour la zone City)
https://www.youtube.com/watch?v=W-gN-rUxtio

Payday 2 Official Soundtrack - #66 8Bit Are Scary (Assault) audio | Overkill (Utilisé pour la zone Main Base)
https://www.youtube.com/watch?v=RVQ5Thg8_4c

Roblox DOORS OST: Guiding Lights [Death Theme] audio | Lightning_Splash (Utilisé pour le Game Over)
https://www.youtube.com/watch?v=qMmp3cdK3ms

Sonic Generations Level Clear audio | Sega (Utilisé pour la scene End)
https://www.youtube.com/watch?v=EVo86lgnCm4

Missile Launche Sound Effect audio | Sound Effect Database (Utilisé pour les missiles)
https://www.youtube.com/watch?v=IpRWVqQm5YU

Menu Game Button Click Sound Effect audio | Sound Effect Database (Utilisé pour les boutons du menu)
https://www.youtube.com/watch?v=yxafINGGm4Y

Item Pick up (Counter Strike Source) audio | Valve
https://www.youtube.com/watch?v=CeKL4sPWFeY


// Substances

Abstact Wall sbsar | Ozan Önen
https://substance3d.adobe.com/community-assets/assets/11e9ef1c2c0b4c758665d486bc06d0050d6d9756

Conveyer Belt Scifi sbsar | Aryan Kulkarni
https://substance3d.adobe.com/community-assets/assets/68565c271f9b22a36482c28e465a449ebdb57993

Hexagon Tiles sbsar | Ozan Önen
https://substance3d.adobe.com/community-assets/assets/876dfa5341139ff5a4f39ed208688d9b54b893c0

Metal Tile sbsar | Nicolás Acevedo
https://substance3d.adobe.com/community-assets/assets/70f65855af3bf0196f7fecfe30496c7bf4fb7a67

Moon Crater sbsar | 3axes 
drive

Perforated Metal Hex sbsar | Simon Nunez
https://substance3d.adobe.com/community-assets/assets/0dff4e0989a2ee1f765e89ddb9545726988cd184

Sci-fi Painted Grating Tiles sbsar | 3axes
drive

Sci-fi panel sbsar | claudia hoozemans
https://substance3d.adobe.com/community-assets/assets/533a8848d960c0aa8d1028c753a602410430413e

Sci-fi retro screen wall sbsar | 3axes
drive

Sci-fi Spaceship Access Cover sbsar | 3axes
drive

Sci-fi Storage Unit Wall sbsar | 3axes
drive

Scifi floor sbsar | Aryan Kulkarni
https://substance3d.adobe.com/community-assets/assets/d1340e301c326e2d1ae8f3b9b29b1bd6dbdeee2e

Scifi Panel sbsar | Wesley McDermott
https://substance3d.adobe.com/community-assets/assets/6f50e1e7aea713bc8b3967d130151e18e63e5c6e

ScifiPanels sbsar | Kimberly MacNeil
https://substance3d.adobe.com/community-assets/assets/3525406c9cbc8e8950ee723482e205a5bcb1519e

Space Shuttle Heat Shield sbsar | 3axes
drive

Space Station Interior Compartment sbsar | 3axes
drive

Space Station Outer Panel sbsar | 3axes
drive

Space Station Thermal Padding sbsar | 3axes
drive