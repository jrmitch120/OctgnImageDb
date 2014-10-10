OctgnImageDb
============

OctgnImageDb is an attempt to make a comprehensive image database for card games supported by the [OCTGN](http://www.octgn.net/ "OCTGN") platform.  Many games ship with censored images.  This program tries to ease the pain of scanning your cards and importing them into OCTGN.

How does it work?
----------------
The program will determine which supported games you have installed and attempt to source images for you.  It does this in one of two ways.

1. It will search a local image cache for a matching card image.  If a match is found, it will import it into the game for you.
2. If an image is not found in the cache, it will attempt to source the image from an online provider.  If found, it will insert it into the appropriate game and store it in the image cache.  

Image providers are currently as follows:

- [NetrunnerDb (It's back!)](http://www.netrunnerdb.com/ "NetrunnerDb") for Android Netrunner.
- [DoomtownDb](http://dtdb.co/ "DoomtownDb") for Doomtown Reloaded.
- [TopTierGaming](http://toptiergaming.com/database.php "TopTierGaming") for Star Wars LCG.

Future Plans Additions
----------------------
- Allow image packs to be imported to the image cache