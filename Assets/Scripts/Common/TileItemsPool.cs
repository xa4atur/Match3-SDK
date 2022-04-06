using System.Collections.Generic;
using Common.Enums;
using Common.Extensions;
using Common.Interfaces;
using Common.Models;
using UnityEngine;

namespace Common
{
    public class TileItemsPool : MonoBehaviour
    {
        [SerializeField] private Transform _gameBoard;

        [Space]
        [SerializeField] private TileModel[] _tiles;

        private Dictionary<TileGroup, GameObject> _tilePrefabs;
        private Dictionary<TileGroup, Queue<IGridTile>> _itemsPool;

        private void Awake()
        {
            _itemsPool = new Dictionary<TileGroup, Queue<IGridTile>>();
            _tilePrefabs = new Dictionary<TileGroup, GameObject>();

            foreach (var tile in _tiles)
            {
                _tilePrefabs.Add(tile.Group, tile.Prefab);
                _itemsPool.Add(tile.Group, new Queue<IGridTile>());
            }
        }

        public IGridTile GetTile(TileGroup group)
        {
            var tiles = _itemsPool[group];
            var tile = tiles.Count == 0 ? CreateTile(_tilePrefabs[group]) : tiles.Dequeue();
            tile.SetActive(true);

            return tile;
        }

        public void ReturnTile(IGridTile tile)
        {
            tile.SetActive(false);
            _itemsPool[tile.Group].Enqueue(tile);
        }

        private IGridTile CreateTile(GameObject tilePrefab)
        {
            return tilePrefab.CreateNew<IGridTile>(parent: _gameBoard);
        }
    }
}