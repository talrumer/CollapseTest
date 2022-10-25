using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Collapse.Blocks
{
    /**
     * Block behavior - default handling of inputs, triggers and animations
     */
    public abstract class Block : MonoBehaviour
    {
        // Public props used by BoardManager
        public BlockType Type;
        public Vector2Int GridPosition;
        public GameObject destroyEffect;

        protected bool IsTriggered;

        /**
         * Start
         */
        private void Start()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, .2f).SetDelay(Random.value * .3f);
        }

        /**
         * OnMouseEnter
         */
        private void OnMouseEnter()
        {
            if (IsTriggered) return;
            transform.DOKill();
            transform.DOScale(Vector3.one * 1.2f, .1f).SetEase(Ease.OutQuad);
        }

        /**
         * OnMouseExit
         */
        private void OnMouseExit()
        {
            if (IsTriggered) return;
            transform.DOKill();
            transform.DOScale(Vector3.one, .1f).SetEase(Ease.OutQuad);
        }

        /**
         * OnMouseUp
         */
        protected virtual void OnMouseUp()
        {
            if (IsTriggered) return;
            BoardManager.Instance.TriggerMatch(this);
        }

        public void prettyDestroySelf()
        {
            BoardManager.Instance.registerDestroy(this);
            if (destroyEffect != null)
            {
                GameObject boomFX = Instantiate(destroyEffect, transform.position, Quaternion.identity);
                Destroy(boomFX, 1f);
            }
            transform.DOScale(Vector3.zero, .1f).SetEase(Ease.OutQuad);
            GameUtils.invokeInStatic(0.1f, () =>
            {
                transform.DOKill();
                Destroy(gameObject);
            });
        }

        /**
         * Trigger the block
         */
        public virtual void Triger(float delay)
        {
            if (IsTriggered) return;
            IsTriggered = true;

            // Clear from board
            destroyBlock(delay);
        }

        public virtual void destroyBlock(float delay = 0)
        {
            BoardManager.Instance.ClearBlockFromGrid(this);

            // Kill game object
            GameUtils.invokeInStatic(delay, () =>
            {
                prettyDestroySelf();
            });
        }
    }
}