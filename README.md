OctgnImageDb
============

OctgnImageDb is an attempt to make a comprehensive image database for card games supported by the [OCTGN](http://www.octgn.net/ "OCTGN") platform.  Many games ship with censored images.  This program tries to ease the pain of scanning your cards and importing them into OCTGN.

How does it work?
----------------
The program will determine which supported games you have installed and attempt to source images for you.  It does this in one of two ways.

1. It will search a local image cache for a matching card image.  If a match is found, it will import it into the game for you.

2. If an image is not found in the cache, it will attempt to source the image from an online provider.  If found, it will insert it into the appropriate game and store it in the image cache.  

Image providers are currently as follows:

- [NetrunnerDb.net](http://netrunnerdb.com/ "NetrunnerDb") for Android Netrunner.
- [DoomtownDb](http://dtdb.co/ "DoomtownDb") for Doomtown Reloaded.
- [ThronesDb](http://thronesdb.com/ "ThronesDb") for Game of Thrones Lcg 2nd Edition.
- [TopTierGaming](http://toptiergaming.com/database.php "TopTierGaming") for Star Wars LCG. **[RETIRED]** - Top Tier's database has gone under. 

Importing image packs (.o8c files)
----------------------------------
If you would like to import image packs into your image cache, drop them in the same directory as OctgnImageDb.exe.  Once started, the program will detect the image packs and ask you if you'd like to import them.

Browsing or editing your cache
-------------------------------
All images are stored in a single directory labeled *ImageCache*.  File names are generated by OCTGN cardId and the appropriate file extension.  

You can replace or delete any image in the cache as you see fit.

A word of caution
-----------------
OctgnImageDb will attempt to download an image for anything not in the *ImageCache* folder.

That means if you have better scans installed than are available online, you'll want to put copies of them in the *ImageCache* folder.  If you don't, they will be overwritten by their online counterparts.  Scans that are already packaged up in .o8c files, can be easily imported as stated above.

Future plans
------------
- TBA