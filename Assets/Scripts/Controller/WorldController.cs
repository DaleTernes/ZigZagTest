using UnityEngine;


namespace ZigZag.Controller
{
    public class WorldController
    {
        readonly TileController _tileController;
        readonly CrystalController _crystalController;


        public WorldController( Object tilePrefab, Object crystalPrefab )
        {
            _crystalController = new CrystalController( crystalPrefab );
            _tileController = new TileController( tilePrefab, _crystalController );
        }


        public void Init( GameParams gameParams )
        {
            _tileController.Cleanup();
            _crystalController.Cleanup();

            _tileController.Init( gameParams.Difficulty );
            _crystalController.Init( gameParams.CrystalGenerationMode );
        }


        public void OnUpdate( Vector3 ballPos )
        {
            _tileController.OnUpdate( ballPos );
        }
    }
}