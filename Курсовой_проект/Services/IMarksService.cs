using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Курсовой_проект.Models;

namespace Курсовой_проект.Services
{
    public interface IMarksService
    {
        void AddMark(MarkModel mark);
        void EditMark(MarkModel mark);
        void RemoveMark(int id);
        List<MarkModel> GetMarks();
        MarkModel GetMark(string mark);
        MarkModel GetMarkById(int id);
    }
}
