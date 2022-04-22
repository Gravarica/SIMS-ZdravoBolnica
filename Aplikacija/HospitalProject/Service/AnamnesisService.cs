﻿using HospitalProject.Model;
using HospitalProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Service
{
    public class AnamnesisService
    {

        private AnamnesisRepository _anamnesisRepository;

        public AnamnesisService(AnamnesisRepository anamnesisRepository)
        {
            _anamnesisRepository = anamnesisRepository;
        }
        
        public IEnumerable<Anamnesis> GetAll()
        {
            return _anamnesisRepository.GetAll();
        }

        public Anamnesis GetById(int id)
        {
            return _anamnesisRepository.GetById(id);
        }

        public void Delete(int id)
        {
            _anamnesisRepository.Delete(id);
        }

        public void Update(Anamnesis anamnesis)
        {
            _anamnesisRepository.Update(anamnesis);
        }

    }
}
