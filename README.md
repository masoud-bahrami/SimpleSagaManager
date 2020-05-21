# SimpleSagaManager


Saga is a mechanism for controlling Long-Live Transactions(LLTs) that spans multiple database transactions or Microservices 
and may not be possible in the usual way for a variety of reasons.

This repository includes a very simple and linear(!) implementation of Saga in dot net core that you can use to get ideas for using or implementing this pattern.

# How to use it

Setup the Saga:
`
var saga=SimpleSagaManager<SharedDataAcroosTasks>.StartWith(task1).Then(task2).Then(task3).Then(task4);
`

Use the Saga: 
`
  await saga.Run(contextData);
`
