from datetime import timedelta

from airflow import DAG
from airflow.operators.dummy import DummyOperator
from airflow.providers.microsoft.winrm.hooks.winrm import WinRMHook
from airflow.providers.microsoft.winrm.operators.winrm import WinRMOperator
from airflow.utils.dates import days_ago

default_args = {
    'owner': 'airflow',
    'email': ['jmuniz1985@gmail.com'],
    'email_on_failure': True,
    'email_on_retry': True,
    'email_on_success': True,
    'retry_exponential_backoff': True,
    'retry_delay' : timedelta(seconds=300),
    'retries': 3
}

with DAG(
    dag_id='FiBigSend',
    default_args=default_args,
    schedule_interval='0 0 * * *',
    start_date=days_ago(2),
    dagrun_timeout=timedelta(minutes=60),
    tags=['windows'],
) as dag:

    cmd = 'ls -l'
    run_this_last = DummyOperator(task_id='run_this_last')

    winRMHook = WinRMHook(ssh_conn_id='ssh_asturias_hml')

    t1 = WinRMOperator(
        task_id="exec",
        command='C:\CMDFI\FICLOSE\fibigsend.bat',
        winrm_hook=winRMHook
        )

    t2 = WinRMOperator(
        task_id="mailconcat", command='C:\CMDFI\FICLOSE\mfimove.bat',
        winrm_hook=winRMHook
        )

    [t1 >> t2] >> run_this_last
