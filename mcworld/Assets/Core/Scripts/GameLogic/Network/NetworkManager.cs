using System;
using System.Collections.Generic;
using Core.Utils;
using Core.Utils.Log;

namespace Core.GameLogic.Network
{
    public class NetworkManager
    {
        private Queue<NetPackage> _InBoundMessageQueue = new Queue<NetPackage>();
        private Queue<NetPackage> _OutBoundMessageQueue = new Queue<NetPackage>();

        private World _World = null;
        private MessageHandler _MessageHandler = null;
        public MessageSender _MessageSender = null;

        public void Init(World world)
        {
            _World = world;

            _MessageHandler = new MessageHandler();
            _MessageHandler.Init(world);

            _MessageSender = new MessageSender();
            _MessageSender.Init(world);
        }

        public void UnInit()
        {
            if (_MessageSender != null)
                _MessageSender.UnInit();

            if (_MessageHandler != null)
                _MessageHandler.UnInit();
        }

        public void OnMessageReceived(int protocol, IntPtr data, int size)
        {
            //LogHelper.DEBUG("NetworkManager", "OnMessageReceived protocol={0} size={1}", protocol, size);
            EnqueueInBoundMessage(protocol, data, size);
        }

        private void EnqueueInBoundMessage(int protocol, IntPtr data, int size)
        {
            NetPackage pkg = new NetPackage();
            pkg._Protocol = protocol;
            pkg._Data = UtilsHelper.IntPtrToBytes(data, size);

            _InBoundMessageQueue.Enqueue(pkg);
        }

        public void EnqueueOutBoundMessage(int protocol, byte[] data)
        {
            NetPackage pkg = new NetPackage();
            pkg._Protocol = protocol;
            pkg._Data = data;

            _OutBoundMessageQueue.Enqueue(pkg);
        }

        private void ProcessInBoundMessages()
        {
            while (_InBoundMessageQueue.Count > 0)
            {
                var pkg = _InBoundMessageQueue.Dequeue();
                _MessageHandler.ProcessMessage(pkg._Protocol, pkg._Data);
            }
        }

        private void ProcessOutBoundMessages()
        {
            while (_OutBoundMessageQueue.Count > 0)
            {
                var pkg = _OutBoundMessageQueue.Dequeue();
                _World._CppCore.SendMessageToServer(pkg._Protocol, pkg._Data);
            }
        }

        public void Active()
        {
            ProcessInBoundMessages();
            ProcessOutBoundMessages();
        }
    }
}
