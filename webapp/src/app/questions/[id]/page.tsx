'use client';

import {getQuestionById} from "@/lib/actions/question-actions";
import {notFound} from "next/navigation";
import QuestionDetailedHeader from "@/app/questions/[id]/QuestionDetailedHeader";
import QuestionContent from "@/app/questions/[id]/QuestionContent";
import AnswerContent from "@/app/questions/[id]/AnswerContent";

type Params = Promise<{ id: string }>

function AnswerHeader(props: { answerCount: number }) {
    return null;
}

export default async function QuestionDetailedPage({params}: { params: Params }) {
    const {id} = await params;
    const question = await getQuestionById(id);

    if (!question) return notFound();
    return (
        <div className='w-full'>
            <QuestionDetailedHeader question={question}/>
            <QuestionContent question={question}/>
            {question.answers.length > 0 && (
                <AnswerHeader answerCount={question.answers.length} />
            )}
            {question.answers.map(answer => (
                <AnswerContent answer={answer} key={answer.id} />
            ))}
        </div>
    )
}