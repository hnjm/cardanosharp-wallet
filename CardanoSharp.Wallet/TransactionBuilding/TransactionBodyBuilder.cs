﻿using CardanoSharp.Wallet.Extensions;
using CardanoSharp.Wallet.Models.Addresses;
using CardanoSharp.Wallet.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardanoSharp.Wallet.TransactionBuilding
{
    public interface ITransactionBodyBuilder: IABuilder<TransactionBody>
    {
        ITransactionBodyBuilder AddInput(byte[] transactionId, uint transactionIndex);
        ITransactionBodyBuilder AddInput(string transactionId, uint transactionIndex);
        ITransactionBodyBuilder AddOutput(byte[] address, ulong coin, ITokenBundleBuilder tokenBundleBuilder = null);
        ITransactionBodyBuilder AddOutput(Address address, ulong coin, ITokenBundleBuilder tokenBundleBuilder = null);
        ITransactionBodyBuilder SetCertificate(ICertificateBuilder certificateBuilder);
        ITransactionBodyBuilder SetFee(ulong fee);
        ITransactionBodyBuilder SetTtl(uint ttl);
        ITransactionBodyBuilder SetMint(ITokenBundleBuilder token);
    }

    public class TransactionBodyBuilder : ABuilder<TransactionBody>, ITransactionBodyBuilder
    {
        private TransactionBodyBuilder()
        {
            _model = new TransactionBody();
        }

        public static ITransactionBodyBuilder Create
        {
            get => new TransactionBodyBuilder();
        }

        public ITransactionBodyBuilder SetCertificate(ICertificateBuilder certificateBuilder)
        {
            _model.Certificate = certificateBuilder.Build();
            return this;
        }

        public ITransactionBodyBuilder AddInput(byte[] transactionId, uint transactionIndex)
        {

            _model.TransactionInputs.Add(new TransactionInput()
            {
                TransactionId = transactionId,
                TransactionIndex = transactionIndex
            });
            return this;
        }

        public ITransactionBodyBuilder AddInput(string transactionIdStr, uint transactionIndex)
        {
            byte[] transactionId = transactionIdStr.HexToByteArray();
            _model.TransactionInputs.Add(new TransactionInput()
            {
                TransactionId = transactionId,
                TransactionIndex = transactionIndex
            });
            return this;
        }

        public ITransactionBodyBuilder AddOutput(Address address, ulong coin, ITokenBundleBuilder tokenBundleBuilder = null)
        {
            return AddOutput(address.GetBytes(), coin, tokenBundleBuilder);
        }

        public ITransactionBodyBuilder AddOutput(byte[] address, ulong coin, ITokenBundleBuilder tokenBundleBuilder = null)
        {
            var outputValue = new TransactionOutputValue()
            {
                Coin = coin
            };

            if (tokenBundleBuilder != null)
            {
                outputValue.MultiAsset = tokenBundleBuilder.Build();
            }

            _model.TransactionOutputs.Add(new TransactionOutput()
            {
                Address = address,
                Value = outputValue
            });
            return this;
        }

        public ITransactionBodyBuilder SetFee(ulong fee)
        {
            _model.Fee = fee;
            return this;
        }

        public ITransactionBodyBuilder SetTtl(uint ttl)
        {
            _model.Ttl = ttl;
            return this;
        }

        public ITransactionBodyBuilder SetMint(ITokenBundleBuilder tokenBuilder)
        {
            _model.Mint = tokenBuilder.Build();
            return this;
        }
    }
}
