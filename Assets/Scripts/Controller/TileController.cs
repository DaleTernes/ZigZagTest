using System;
using System.Collections.Generic;
using PubSub;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace ZigZag.Controller
{
    public class TileController : PubSubEntity
    {
        readonly CrystalController _crystalController;
        readonly HashSet<Vector3> _positionsHashSet = new HashSet<Vector3>();
        readonly HashSet<ITile> _tilesHashSet = new HashSet<ITile>();
        readonly IPooler _pooler;
        DifficultyLevel _difficulty;
        float _fallCheckTimer;
        Vector3 _lastTilePos;


        public TileController( Object tilePrefab, CrystalController crystalController )
        {
            var holder = new GameObject( "Tiles" );
            _crystalController = crystalController;
            _pooler = new PoolController( tilePrefab, holder.transform );
        }


        public void Init( DifficultyLevel difficulty )
        {
            _lastTilePos = new Vector3( 1, 0, 1 );
            _difficulty = difficulty;
            CreateStartPad();
            CreatePath();
        }


        void CreateStartPad()
        {
            for ( int i = -1; i < 2; i++ )
            for ( int z = -1; z < 2; z++ )
                PlaceTile( new Vector3( i, 0, z ) );
        }


        void CreatePath()
        {
            for ( int i = 0; i < 30; i++ )
            {
                var pos = _lastTilePos;
                bool movingByX = Random.Range( 0f, 1f ) < .5f;
                if ( movingByX )
                    pos.x++;
                else
                    pos.z++;

                PlaceTile( pos );
                _crystalController.PlaceCrystal( pos );
                _lastTilePos = pos;

                switch ( _difficulty )
                {
                    case DifficultyLevel.Easy:
                        if ( movingByX )
                        {
                            PlaceTile( new Vector3( pos.x, pos.y, pos.z + 1 ) );
                            PlaceTile( new Vector3( pos.x, pos.y, pos.z - 1 ) );
                        }
                        else
                        {
                            PlaceTile( new Vector3( pos.x + 1, pos.y, pos.z ) );
                            PlaceTile( new Vector3( pos.x - 1, pos.y, pos.z ) );
                        }

                        break;

                    case DifficultyLevel.Medium:
                        if ( movingByX )
                            pos.z++;
                        else
                            pos.x++;

                        PlaceTile( pos );
                        break;

                    case DifficultyLevel.Hard:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }


        public void OnUpdate( Vector3 ballPos )
        {
            PathCheck( ballPos );
            foreach ( var tile in _tilesHashSet )
                tile.OnUpdate( ballPos );
        }


        void PathCheck( Vector3 ballPos )
        {
            float dist = Vector3.Distance( ballPos, _lastTilePos );
            if ( dist < 15 ) CreatePath();
        }


        void PlaceTile( Vector3 position )
        {
            if ( _positionsHashSet.Contains( position ) ) return;

            _positionsHashSet.Add( position );
            var tile = (ITile) _pooler.GetItem();
            tile.Init( _pooler, position );
            
            if ( !_tilesHashSet.Contains( tile ) ) 
                _tilesHashSet.Add( tile );
        }


        public void Cleanup()
        {
            _pooler.Clear();
            _tilesHashSet.Clear();
            _positionsHashSet.Clear();
        }
    }
}