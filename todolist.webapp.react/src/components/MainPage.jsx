import { useState } from 'react'
import PageButton from './PageButton.jsx'
import TodoListTable from '../todos/TodoListTable.jsx'
import { LogOutButton } from './LogOutButton.jsx'
import { Register } from '../auth/Register.jsx'
import TaskView from '../tasks/TaskView.jsx'

function MainPage() {
    const [todoListId, setTodoListId] = useState(null);

    return (
        <div>
            <TodoListTable
                setTodoListId={setTodoListId}
            />

            <TaskView
                todoListId={todoListId}
            />

            <LogOutButton></LogOutButton>
        </div>
    )
}

export default MainPage