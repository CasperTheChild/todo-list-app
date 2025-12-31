import { useState } from 'react'
import TaskDescription from './TaskDescription.jsx'
import TaskList from './TaskList.jsx'

export default function TaskView({ todoListId }) {
    const [currentTaskId, setCurrentTaskId] = useState(-1);
    const [taskEdited, setTaskEdited] = useState(false);

    return (
        <div>
            <TaskList
                todoListId={todoListId}
                setEditingTaskId={setCurrentTaskId}
                taskEdited={taskEdited}
                setTaskEdited={setTaskEdited}
            />
            
            {currentTaskId != -1 &&
                <TaskDescription
                    todoListId={todoListId}
                    taskId={currentTaskId}
                    setTaskEdited={setTaskEdited}
            />}
            
        </div>
    )
}