﻿namespace Company.G03.PL.Services
{
    public class TransientService : ITransientService
    {
        public Guid Guid { get; set; }
        public TransientService()
        {
            Guid = Guid.NewGuid();
        }

        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
