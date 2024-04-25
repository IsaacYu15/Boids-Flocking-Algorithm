# FLOCKING ALGORITHM
This project aims to re-create the behaviour of schools of fish based upon the core principles of the flocking algorithm:
   - alignment (each fish will try to align them selfs with neighbouring fish)
   - cohesion  (fish will try to swim / group together)
   - avoidance (fish are not allowed to bump into one another)
   - bounding  (fish must stay within a certain region)
     with each of these behaviours having a weight/importance scale for each fish.
     
Flocking algorithms are useful when it comes to re-creating group behaviour as each unit is allowed to "think for it's own / adapt to it's surroundings" rather 
than following a pre-defined path or having a singular parent manage them all. 

For video updates of the entire process see my [playlist!]([https://www.youtube.com/watch?v=F-7OTlQRWRY&list=PLNU3z4IRiDwNV2LOxBC6R3CmT7_a09eHN)]
Watch the latest iteration in [this video!](https://www.youtube.com/watch?v=hVDA2jKtwXM&list=PLNU3z4IRiDwNV2LOxBC6R3CmT7_a09eHN&index=2)

## FUTURE POINTS OF EXPLORATION
- A better way to determine the weights for alignment, cohesion, avoidance, and bounding
- External motivations (e.g. if a shark swims towards the fish, they should avoid the shark and re-group when the shark is gone)
- Schools of fish can follow a spline instead of just staying within fixed boundaries
 
## BUILT USING
- [Unity](https://unity.com/download) - A game engine designed for real-time 3D & 2D projects

## PREREQUISITES
- [Unity](https://unity.com/download) Version 2021.3.11f1. 
- [C# in VsCode](https://code.visualstudio.com/docs/languages/csharp)
  
## TRY IT YOURSELF!
  1. Download the project as a ZipFile
  2. Unzip the file
  3. Open up the UnityHub and select the project
