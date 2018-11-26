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
        private readonly IImagesLogic _imagesLogic;

        public SpeakersLogic(IRepository<Speaker> speakersRepository, IImagesLogic imagesLogic)
        {
            _speakersRepository = speakersRepository;
            _imagesLogic = imagesLogic;
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

        public IEnumerable<Speaker> GetSpeakers()
        {
            try
            {
                return _speakersRepository.List();
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

        public bool SaveImage(int speakerId, FileInfo image)
        {
            try
            {
                var speakersBlob = "";
                var imageUrl = _imagesLogic.LoadImage(image, speakersBlob);

                var speaker = _speakersRepository.FindById(speakerId);
                speaker.Image = imageUrl;
                _speakersRepository.Update(speaker);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion
    }
}
