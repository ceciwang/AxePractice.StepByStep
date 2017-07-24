using System;

namespace Manualfac
{
    class Disposer : Disposable
    {
        #region Please implements the following methods

        /*
         * The disposer is used for disposing all disposable items added when it is disposed.
         */
        HastSet<object> items = new HastSet<object>(){};
        
        public void AddItemsToDispose(object item)
        {
            this.items.Add(item);
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing){
                this.items = null;
            }
        }

        #endregion
    }
}