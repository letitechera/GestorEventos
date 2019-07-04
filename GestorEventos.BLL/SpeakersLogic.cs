using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GestorEventos.BLL.Interfaces;
using GestorEventos.DAL.Repositories.Interfaces;
using GestorEventos.Models.Entities;

namespace GestorEventos.BLL
{
    public class SpeakersLogic : ISpeakersLogic
    {
        private readonly IRepository<Speaker> _speakersRepository;

        public SpeakersLogic(IRepository<Speaker> speakersRepository)
        {
            _speakersRepository = speakersRepository;
        }

        #region Speakers

        public bool SaveSpeaker(Speaker speaker, bool update = false)
        {
            try
            {
                if (update)
                {
                    _speakersRepository.Update(speaker);
                }
                else
                {
                    _speakersRepository.Add(speaker);
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool DeleteSpeaker(int speakerId)
        {
            try
            {
                _speakersRepository.Delete(speakerId);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Speaker> GetSpeakers(int activityId)
        {
            try
            {
                return _speakersRepository.List(s => s.ActivityId == activityId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Speaker GetSpeaker(int speakerId)
        {
            try
            {
                return _speakersRepository.FindById(speakerId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }
}
