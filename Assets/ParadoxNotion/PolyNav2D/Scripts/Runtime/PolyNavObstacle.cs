using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

namespace PolyNav{

	[DisallowMultipleComponent]
	[AddComponentMenu("Navigation/PolyNavObstacle")]
	///Place on a game object to act as an obstacle
	public class PolyNavObstacle : MonoBehaviour {

		public enum ShapeType
		{
            Tilemap,
			Polygon,
			Box,
			Composite
		}

		///Raised when the state of the obstacle is changed (enabled/disabled).
		public static event System.Action<PolyNavObstacle, bool> OnObstacleStateChange;

		[Tooltip("The Shape used. Changing this will also change the Collider2D component type.")]
		public ShapeType shapeType = ShapeType.Polygon;
		[Tooltip("Added extra offset radius.")]
		public float extraOffset;
		[Tooltip("Inverts the polygon (done automatically if collider already exists due to a sprite).")]
		public bool invertPolygon = false;

		private Collider2D _collider;
		private Collider2D myCollider{
			get {return _collider != null? _collider : _collider = GetComponent<Collider2D>();}
		}

        // cache of number of cells in tilemap
        private List<Vector2[]> tilemapColliderPaths;

		///The number of paths defining the obstacle
		public int GetPathCount(){
			if (myCollider is BoxCollider2D){ return 1; }
			if (myCollider is PolygonCollider2D){ return (myCollider as PolygonCollider2D).pathCount; }
			if (myCollider is CompositeCollider2D){ return (myCollider as CompositeCollider2D).pathCount; }
            if (myCollider is TilemapCollider2D) {
                if (tilemapColliderPaths == null) InitializeTilemapPath();
                return tilemapColliderPaths.Count;
            }
			return 0;
		}

		///Returns the points defining a path
		public Vector2[] GetPathPoints(int index){
			Vector2[] points = null;
			if (myCollider is BoxCollider2D){
				var box = (BoxCollider2D)myCollider;
				var tl = box.offset + (new Vector2(-box.size.x, box.size.y)/2);
				var tr = box.offset + (new Vector2(box.size.x, box.size.y)/2);
				var br = box.offset + (new Vector2(box.size.x, -box.size.y)/2);
				var bl = box.offset + (new Vector2(-box.size.x, -box.size.y)/2);
				points = new Vector2[]{tl, tr, br, bl};
			}

			if (myCollider is PolygonCollider2D){
				var poly = (PolygonCollider2D)myCollider;
				points = poly.GetPath(index);
			}

			if (myCollider is CompositeCollider2D){
				var comp = (CompositeCollider2D)myCollider;
				points = new Vector2[comp.GetPathPointCount(index)];
				comp.GetPath(index, points);
			}

            if (myCollider is TilemapCollider2D)
            {
                points = tilemapColliderPaths[index];
            }

            if (invertPolygon && points != null){ System.Array.Reverse(points); }
			return points;
		}

		void Reset(){
			
			if (myCollider == null){
				gameObject.AddComponent<PolygonCollider2D>();
				invertPolygon = true;
			}

			if (myCollider is PolygonCollider2D){
				shapeType = ShapeType.Polygon;
			}
			
			if (myCollider is BoxCollider2D){
				shapeType = ShapeType.Box;
			}

			if (myCollider is CompositeCollider2D){
				shapeType = ShapeType.Composite;
			}

            if (myCollider is TilemapCollider2D)
            {
                shapeType = ShapeType.Tilemap;
            }

		}

		void OnEnable(){
			if (OnObstacleStateChange != null){
				OnObstacleStateChange(this, true);
			}
		}

		void OnDisable(){
			if (OnObstacleStateChange != null){
				OnObstacleStateChange(this, false);
			}
		}

		void Awake(){
			transform.hasChanged = false;
		}

        public void InitializeTilemapPath()
        {
            tilemapColliderPaths = new List<Vector2[]>();

            Tilemap tilemap = GetComponent<Tilemap>();
            if (tilemap != null && myCollider != null && myCollider is TilemapCollider2D) {
                // go through each cell
                BoundsInt bounds = tilemap.cellBounds;
                    
                for (int y = bounds.position.y; y < bounds.size.y; ++y)
                {
                    for (int x = bounds.position.x; x < bounds.size.x; ++x)
                    {
                        // find out if it has a collider
                        Vector3Int cellPosition = new Vector3Int(x, y, 0);
                        if (tilemap.GetColliderType(cellPosition) != Tile.ColliderType.None)
                        {

                            // convert to world space
                            Vector3 worldPosition = tilemap.GetCellCenterWorld(cellPosition);
                            Vector3 size = tilemap.cellSize;

                            var tl = new Vector2(worldPosition.x - size.x / 2, worldPosition.y + size.y / 2);
                            var tr = new Vector2(worldPosition.x + size.x / 2, worldPosition.y + size.y / 2);
                            var br = new Vector2(worldPosition.x + size.x / 2, worldPosition.y - size.y / 2);
                            var bl = new Vector2(worldPosition.x - size.x / 2, worldPosition.y - size.y / 2);
                            tilemapColliderPaths.Add(new Vector2[] { tl, tr, br, bl });
                        }
                    }
                }
            }
        }
	}
}