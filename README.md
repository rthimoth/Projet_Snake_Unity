# Projet_Snake_Unity

Ce projet vise à reproduire un jeu similaire à "Snake".
La caméra reste fixe et le joueur peut seulement changer de direction, le but est de
rester en vie le plus longtemps et ramassé le plus de pommes possible


# README - Projet Jeu Snake sur Unity

## Description
Ce projet est une implémentation du jeu classique "Snake" développé sur Unity. Le jeu consiste à diriger un serpent pour qu'il mange des objets apparaissant aléatoirement sur l'écran.
À chaque objet mangé, le serpent grandit, et le jeu se termine si le serpent heurte les murs ou son propre corps.

## Configuration Requise
- Unity 2020.3 (ou version ultérieure)
- .NET Framework 4.6 (ou version ultérieure)

## Installation
1. Clonez le dépôt ou téléchargez l'archive ZIP du projet.
2. Ouvrez Unity Hub et sélectionnez 'Ajouter'.
3. Naviguez jusqu'au dossier du projet et cliquez sur 'Sélectionner un dossier'.
4. Une fois le projet ouvert dans Unity, allez dans `File > Build Settings` pour générer le jeu.

## Comment Jouer
- Utilisez les touches fléchées du clavier pour déplacer le serpent.
- Mangez des pommes pour augmenter la taille du serpent.
- Évitez de heurter les murs, les pièges ou le corps du serpent.
- Essayez d'obtenir le score le plus élevé possible
- Faire tous les niveaux

## Fonctionnalités:
- Changement de direction: Avant, arrière, gauche, droite
- Une pomme qui apparaît
- Le joueur peut ramasser la pomme pour agrandir son serpent
- 3 niveaux différents avec des obstacles différents
- Pièges qui tue le joueur (Au moins 2)
- Deux évènements aléatoires : Vie Supplémentaire , Contrôles Inversés
- Ecran de démarrage avec: Jouer, Options, Quitter
- Ecran de score quand le joueur meurt et pendant la partie

## Événements Aléatoires

### Vie Supplémentaire
- **Description** : Accorde au joueur une vie supplémentaire.

### Contrôles Inversés
- **Description** : Inverse temporairement les commandes de contrôle.
- **Bonus** : +1 Bonus de point quand pomme mangé durant cette event

## Auteur
Clément PENOT & Thimothee RANVIN
