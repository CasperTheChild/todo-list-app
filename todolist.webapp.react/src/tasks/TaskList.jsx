import { useState, useEffect } from 'react';
import { getPagedTasks, createTask, deleteTask } from '../api/TaskApi'
import { useAuth } from '../auth/AuthContext'

export default function TaskList({todoListId, setEditingTaskId, taskEdited, setTaskEdited}) {
    const { token } = useAuth();

    const [ prevDefVal, setPrevDefVal ] = useState(0);

    const [ hasMore, setHasMore ] = useState(false);

    const [ pageNum, setPageNum ] = useState(1);
    const [ pageSize, setPageSize ] = useState(4);

    const [ tasks, setTasks ] = useState({
        items: [],
        totalItems: 0,
        pageNum: 1,
        pageSize: 4,
        totalPages: 0,
    })

    useEffect(() => {
        if (!todoListId) return;

        getPagedTasks(todoListId, pageNum, pageSize, token)
            .then(data => {
                setTasks(data);
                setPrevDefVal(data.totalItems);
                setTaskEdited(false);
            });
        
    }, [todoListId, pageNum, pageSize, taskEdited]);

    async function getNextTasks() {
        if (pageNum >= tasks.totalPages) return;

        const nextPage = pageNum + 1;
        const data = await getPagedTasks(todoListId, nextPage, pageSize, token);

        setPageNum(nextPage);
        setTasks(prev => ({
            ...prev,
            ...data,
            items: [...prev.items, ...data.items]
        }));
    }

    async function handleDeleteTask(taskId) {
        await deleteTask(todoListId, taskId, token);

        getPagedTasks(todoListId, pageNum, pageSize, token)
            .then(data => setTasks(data));
    }

    async function handleCreateTask() {
        const newTask = {
            title: `Task #${prevDefVal + 1}`,
            description: ``,
            isCompleted: false,
            startDate: new Date().toISOString(),
        };

        setPrevDefVal(prevDefVal + 1);

        await createTask(todoListId, newTask, token);
        
        getPagedTasks(todoListId, pageNum, pageSize, token)
            .then(data => setTasks(data));
    }

    return (
        <div>
            <h1>Tasks</h1>
            <table>
                <thead>
                </thead>
                <tbody>
                    {tasks.items.length > 0 &&
                        tasks.items.map(task => (
                            <tr key={task.id}>
                                <td>{task.title}</td>
                                <td>{task.description}</td>
                                <td>{task.startDate}</td>
                                <td>{task.endDate}</td>
                                <td>{task.isCompleted}</td>
                                <td><button onClick={() => handleDeleteTask(task.id)}>Delete</button></td>
                                <td><button onClick={() => setEditingTaskId(task.id)}>Edit</button></td>
                            </tr>
                        ))}
                </tbody>
            </table>

            <button onClick={() => handleCreateTask()}>New Task</button>
            <button onClick={() => getNextTasks()}>Next Tasks</button>
        </div>
    )
}