using GestorEventos.BLL.Interfaces;
using GestorEventos.DAL.Repositories.Interfaces;
using GestorEventos.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GestorEventos.BLL
{
    public class AttendantsLogic : IAttendantsLogic
    {
        IRepository<Attendant> _attendantsRepository;

        public AttendantsLogic(IRepository<Attendant> attendantsRepository)
        {
            _attendantsRepository = attendantsRepository;
        }

        #region Attendants

        public bool SaveAttendant(Attendant attendant, bool update = false)
        {
            try
            {
                if (update)
                {
                    _attendantsRepository.Update(attendant);
                }
                else
                {
                    var existant = _attendantsRepository
                    .List()
                    .FirstOrDefault(x => x.Email.ToLower() == attendant.Email.ToLower());

                    if (existant == null)
                    {
                        _attendantsRepository.Add(attendant);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool DeleteAttendant(int attendantId)
        {
            try
            {
                _attendantsRepository.Delete(attendantId);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Attendant> GetAttendants()
        {
            try
            {
                return _attendantsRepository.List();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Attendant GetAttendant(int attendantId)
        {
            try
            {
                return _attendantsRepository.FindById(attendantId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Attendant ExistsAttendant(string email)
        {
            var at = _attendantsRepository.List(a => a.Email == email || !string.IsNullOrEmpty(a.Email))
                .FirstOrDefault();
            return at;
        }

        #endregion
    }
}
