﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.GameService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GameService.IGameService", CallbackContract=typeof(Client.GameService.IGameServiceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IGameService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/DiscardCard", ReplyAction="http://tempuri.org/IGameService/DiscardCardResponse")]
        void DiscardCard(int cardToDiscard);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/DiscardCard", ReplyAction="http://tempuri.org/IGameService/DiscardCardResponse")]
        System.Threading.Tasks.Task DiscardCardAsync(int cardToDiscard);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/DrawCard", ReplyAction="http://tempuri.org/IGameService/DrawCardResponse")]
        void DrawCard();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/DrawCard", ReplyAction="http://tempuri.org/IGameService/DrawCardResponse")]
        System.Threading.Tasks.Task DrawCardAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/Join", ReplyAction="http://tempuri.org/IGameService/JoinResponse")]
        int Join(Service.Contracts.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/Join", ReplyAction="http://tempuri.org/IGameService/JoinResponse")]
        System.Threading.Tasks.Task<int> JoinAsync(Service.Contracts.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/SelectTile", ReplyAction="http://tempuri.org/IGameService/SelectTileResponse")]
        void SelectTile(int tile, int card);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/SelectTile", ReplyAction="http://tempuri.org/IGameService/SelectTileResponse")]
        System.Threading.Tasks.Task SelectTileAsync(int tile, int card);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/StartGame", ReplyAction="http://tempuri.org/IGameService/StartGameResponse")]
        void StartGame();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/StartGame", ReplyAction="http://tempuri.org/IGameService/StartGameResponse")]
        System.Threading.Tasks.Task StartGameAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGameService/PlayerSelectedTile")]
        void PlayerSelectedTile(int playerId, Service.Contracts.Color color);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGameService/PlayerJoinedGame")]
        void PlayerJoinedGame(int playerid, string playerName, Service.Contracts.Color color);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGameService/YourTurn")]
        void YourTurn();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGameService/PlayerWon")]
        void PlayerWon(int playerId, string playerName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGameService/CardRemoved")]
        void CardRemoved(int card);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGameService/CardAdded")]
        void CardAdded(int card);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameServiceChannel : Client.GameService.IGameService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GameServiceClient : System.ServiceModel.DuplexClientBase<Client.GameService.IGameService>, Client.GameService.IGameService {
        
        public GameServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public GameServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public GameServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GameServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GameServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void DiscardCard(int cardToDiscard) {
            base.Channel.DiscardCard(cardToDiscard);
        }
        
        public System.Threading.Tasks.Task DiscardCardAsync(int cardToDiscard) {
            return base.Channel.DiscardCardAsync(cardToDiscard);
        }
        
        public void DrawCard() {
            base.Channel.DrawCard();
        }
        
        public System.Threading.Tasks.Task DrawCardAsync() {
            return base.Channel.DrawCardAsync();
        }
        
        public int Join(Service.Contracts.Player player) {
            return base.Channel.Join(player);
        }
        
        public System.Threading.Tasks.Task<int> JoinAsync(Service.Contracts.Player player) {
            return base.Channel.JoinAsync(player);
        }
        
        public void SelectTile(int tile, int card) {
            base.Channel.SelectTile(tile, card);
        }
        
        public System.Threading.Tasks.Task SelectTileAsync(int tile, int card) {
            return base.Channel.SelectTileAsync(tile, card);
        }
        
        public void StartGame() {
            base.Channel.StartGame();
        }
        
        public System.Threading.Tasks.Task StartGameAsync() {
            return base.Channel.StartGameAsync();
        }
    }
}