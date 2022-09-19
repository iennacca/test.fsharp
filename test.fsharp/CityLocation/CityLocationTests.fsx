#if INTERACTIVE
#load "CityLocationUnion.fs"
#endif

open CityLocationUnion

// let tcity = "London", "51.5074N", "0.1278W"    
let uCoords = Coordinates ( latval=51.5074, latdir=LatDirection.N, longval=0.1278, longdir=LongDirection.W )
let uCity = City (name="London", loc=uCoords)

showCoordinates uCoords

let (City(n,l)) = uCity

showCoordinates l

// ------------------

#if INTERACTIVE
#load "CityLocationRecord.fs"
#endif

open CityLocationRecord

// let tcity = "London", "51.5074N", "0.1278W"    
let rCoords = { latval=51.5074; latdir=LatDirection.N; longval=0.1278; longdir=LongDirection.W }
let rCity = { name="London"; loc=rCoords }

showCoordinates rCoords
showCoordinates rCity.loc
