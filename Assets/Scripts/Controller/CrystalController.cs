using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace ZigZag.Controller
{
    public class CrystalController
    {
        readonly IPooler _pooler;
        CrystalGenerationMode _mode;
        int _tileCounter;
        int _orderedModeCounter;
        int _tilePositionInBlock;


        public CrystalController( Object crystalPrefab )
        {
            var holder = new GameObject( "Crystals" );
            _pooler = new PoolController( crystalPrefab, holder.transform );
        }


        public void Init( CrystalGenerationMode crystalGenerationMode )
        {
            _mode = crystalGenerationMode;
        }


        public void PlaceCrystal( Vector3 pos )
        {
            if ( _tileCounter == 0 )
                _tilePositionInBlock = ChoosePosition();

            if ( _tilePositionInBlock == _tileCounter )
                CreateCrystal( pos );

            _tileCounter++;

            if ( _tileCounter > 4 ) _tileCounter = 0;
        }


        int ChoosePosition()
        {
            switch ( _mode )
            {
                case CrystalGenerationMode.Random:
                    return Random.Range( 0, 5 );

                case CrystalGenerationMode.Ordered:
                    _orderedModeCounter++;
                    if ( _orderedModeCounter > 5 ) _orderedModeCounter = 1;

                    return _orderedModeCounter - 1;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        void CreateCrystal( Vector3 position )
        {
            position.y += .75f;

            var collectable = GetCollectable();
            collectable.Init( _pooler, position );
        }


        ICollectable GetCollectable()
        {
            return (ICollectable) _pooler.GetItem();
        }


        public void Cleanup()
        {
            _pooler.Clear();
        }
    }
}