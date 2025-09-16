using Microsoft.VisualBasic;

namespace HomeServer.Core
{
    public enum  PriorityLevel
    {
        Low = 0, 
        Medium = 1, 
        High = 2, 
        Critical = 3

    }

    public class ToDo
    {        
        private DateTime? _updatedAt;
        private DateTime? _completedAt;
        private DateTime? _dueDate;
        private DateTime? _startedAt;
        private bool _isRecurring = false;
        private readonly List<string> Notes = [];
        private readonly Dictionary<string, bool> SubTasks = [];
        public DateTime CreatedAt { get; } = DateAndTime.Now;     
        public string? Title { get; set; }
        public string? Description { get; set; }
        public PriorityLevel Priority { get; set; } = PriorityLevel.Low;

        public bool IsCompleted
        {
            get
            {
                return _completedAt != null;
            }
            set
            {
                if (value && _completedAt == null)
                {
                    _completedAt = DateTime.Now;
                    _updatedAt = DateTime.Now;
                }
                else if (!value && _completedAt != null)
                {
                    _completedAt = null;
                    _updatedAt = DateTime.Now;
                }
            }
        }

        public bool IsRecurring 
        { 
            get
            {
                 return _isRecurring;
            }
            set
            {
                if (value != _isRecurring)
                {
                    if (value)
                    {
                        _startedAt = DateTime.Now;
                        _updatedAt = DateTime.Now;                    
                    }
                    else
                    {
                        _startedAt = null;
                    }
                    _isRecurring = value;
                }                    
            }
        }        

        public DateTime? UpdatedAt 
        { 
            get 
            { 
                if (_updatedAt != null)
                {
                    return _updatedAt;
                }  
                else                 {
                    return CreatedAt;
                }
            } 
        }

        public DateTime? CompletedAt 
        {
            get
            {
                return _completedAt;
            }
        }

        public DateTime? DueDate 
        {
            get
            {
                return _dueDate;
            }
            set
            {
                if (value != null && value < CreatedAt)
                {
                    throw new ArgumentException("Due date cannot be earlier than creation date.");
                }
                else
                {
                    _updatedAt = DateTime.Now;
                    _dueDate = value;
                }
            }
        }

        public DateTime? StartedAt 
        { 
            get
            {
                return _startedAt;
            }
        }

        public void AddSubTask(string subTask)
        {
            SubTasks.Add(subTask, false);
            _updatedAt = DateTime.Now;
        }

        public void RemoveSubTask(string subTask)
        {
            SubTasks.Remove(subTask);
            _updatedAt = DateTime.Now;
        }

        public bool CompleteSubTask(string subTask)
        {
            if (SubTasks.ContainsKey(subTask))
            {
                SubTasks[subTask] = true;
                _updatedAt = DateTime.Now;
            }
            if (SubTasks.All(st => st.Value))
            {
                IsCompleted = true;
                return true;
            }
            return false;
        }

        public void UnmarkSubtaskAsCompleted(string subTask)
        {
            if (SubTasks.ContainsKey(subTask))
            {
                SubTasks[subTask] = false;
                if (IsCompleted)
                {
                    IsCompleted = false;
                }
                _updatedAt = DateTime.Now;
            }
        }

        public Dictionary<string, bool> GetSubTasks()
        {
            return SubTasks;
        }

        public void AddNote(string note)
        {
            Notes.Add(note);
            _updatedAt = DateTime.Now;
        }

        public void RemoveNote(string note)
        {
            Notes.Remove(note);
            _updatedAt = DateTime.Now;
        }

        public List<string> GetNotes()
        {
            return Notes;
        }
    }
}
