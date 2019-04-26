using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using DataObjects;

namespace LogicLayerTest
{
	[TestClass]
	public class MessageManagerTests
	{
		IMessageManager _messageManager;
		IThreadManager _threadManager;

		[TestInitialize]
		public void TestSetup()
		{
			//Initialize these two manager classes, because the repo for the messages is below
			//the thread manager.
			_messageManager = new RealMessageManager(AppData.DataStoreType.mock);
			_threadManager = new RealThreadManager(AppData.DataStoreType.mock);
		}

		//test CreateNewMessage(Message message) with bad message
		//test CreateNewMessage(Message message) with good message
		//test CreateNewMessage(Message message) with bad message

		//test RetrieveThreadMessages(IMessageThread thread) with not found thread
		//test RetrieveThreadMessages(IMessageThread thread) with good thread

		//test RetrieveNewestThreadMessage(IMessageThread thread) with not found thread
		//test RetrieveNewestThreadMessage(IMessageThread thread) with good thread

		//test CreateNewReply(IMessageThread thread, Message message) with good thread good message
		//test CreateNewReply(IMessageThread thread, Message message) with good thread bad message
		//test CreateNewReply(IMessageThread thread, Message message) with bad thread bad message
		//test CreateNewReply(IMessageThread thread, Message message) with bad thread good message

		[TestCleanup]
		public void TestTearDown()
		{
			_messageManager = null;
		}
	}
}
