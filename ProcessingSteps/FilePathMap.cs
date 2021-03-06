﻿using AppDynamics.Dexter.DataObjects;
using AppDynamics.Dexter.ReportObjects;
using System;
using System.Collections.Generic;
using System.IO;

namespace AppDynamics.Dexter.ProcessingSteps
{
    public class FilePathMap
    {

        #region Constants for the folder and file names of data extract

        private const string DATA_FOLDER_NAME = "Data";

        // Parent Folder names
        private const string ENTITIES_FOLDER_NAME = "ENT";
        private const string SIM_ENTITIES_FOLDER_NAME = "SIMENT";
        private const string DB_APPLICATION_FOLDER_NAME = "DBMonAll.0";
        private const string DB_ENTITIES_FOLDER_NAME = "DBENT";
        private const string DB_DATA_FOLDER_NAME = "DBDATA";
        private const string CONFIGURATION_FOLDER_NAME = "CFG";
        private const string METRICS_FOLDER_NAME = "METR";
        private const string SNAPSHOTS_FOLDER_NAME = "SNAP";
        private const string EVENTS_FOLDER_NAME = "EVT";
        private const string SA_EVENTS_FOLDER_NAME = "SAEVT";
        private const string ACTIVITYGRID_FOLDER_NAME = "FLOW";
        private const string CONFIGURATION_COMPARISON_FOLDER_NAME = "CMPR";
        private const string PROCESSES_FOLDER_NAME = "PROC";
        private const string QUERIES_FOLDER_NAME = "QUERY";
        private const string RBAC_REPOSITORY_FOLDER_NAME = "RBAC.0";
        private const string RBAC_FOLDER_NAME = "RBAC";

        // Metadata file names
        private const string EXTRACT_CONFIGURATION_APPLICATION_FILE_NAME = "configuration.xml";
        private const string EXTRACT_CONFIGURATION_APPLICATION_SEP_FILE_NAME = "seps.json";
        private const string EXTRACT_CONFIGURATION_DEVELOPER_MODE_NODES_FILE_NAME = "devmode.json";
        private const string EXTRACT_CONFIGURATION_CONTROLLER_FILE_NAME = "settings.json";
        private const string EXTRACT_CONTROLLER_VERSION_FILE_NAME = "controllerversion.xml";

        private const string EXTRACT_ENTITY_APPLICATIONS_FILE_NAME = "applications.json";
        private const string EXTRACT_ENTITY_APPLICATION_FILE_NAME = "application.json";
        private const string EXTRACT_ENTITY_TIERS_FILE_NAME = "tiers.json";
        private const string EXTRACT_ENTITY_NODES_FILE_NAME = "nodes.json";
        private const string EXTRACT_ENTITY_BUSINESS_TRANSACTIONS_FILE_NAME = "businesstransactions.json";
        private const string EXTRACT_ENTITY_BACKENDS_FILE_NAME = "backends.json";
        private const string EXTRACT_ENTITY_BACKENDS_DETAIL_FILE_NAME = "backendsdetail.json";
        private const string EXTRACT_ENTITY_SERVICE_ENDPOINTS_FILE_NAME = "serviceendpoints.json";
        private const string EXTRACT_ENTITY_SERVICE_ENDPOINTS_DETAIL_FILE_NAME = "serviceendpointsdetail.json";
        private const string EXTRACT_ENTITY_ERRORS_FILE_NAME = "errors.json";
        private const string EXTRACT_ENTITY_INFORMATION_POINTS_FILE_NAME = "informationpoints.json";
        private const string EXTRACT_ENTITY_INFORMATION_POINTS_DETAIL_FILE_NAME = "informationpointsdetail.json";
        private const string EXTRACT_ENTITY_NODE_RUNTIME_PROPERTIES_FILE_NAME = "node.{0}.json";
        private const string EXTRACT_ENTITY_NODE_METADATA_FILE_NAME = "nodemeta.{0}.json";
        private const string EXTRACT_ENTITY_BACKEND_TO_DBMON_MAPPING_FILE_NAME = "dbmonmap.{0}.json";
        private const string EXTRACT_ENTITY_BACKEND_TO_TIER_MAPPING_FILE_NAME = "backendmapping.json";

        // SIM metadata file names
        private const string EXTRACT_ENTITY_GROUPS_FILE_NAME = "groups.json";
        private const string EXTRACT_ENTITY_MACHINES_FILE_NAME = "machines.json";
        private const string EXTRACT_ENTITY_MACHINE_FILE_NAME = "machine.{0}.json";
        private const string EXTRACT_ENTITY_DOCKER_CONTAINERS_FILE_NAME = "dockercontainer.{0}.json";
        private const string EXTRACT_ENTITY_SERVICE_AVAILABILITIES_FILE_NAME = "serviceavailabilities.json";
        private const string EXTRACT_ENTITY_SERVICE_AVAILABILITY_FILE_NAME = "serviceavailability.{0}.json";

        // SIM process file names
        private const string EXTRACT_PROCESSES_FILE_NAME = "proc.{0}.{1:yyyyMMddHHmm}-{2:yyyyMMddHHmm}.json";

        // DB metadata file names
        private const string EXTRACT_COLLECTOR_DEFINITIONS_FILE_NAME = "collectordefinitions.json";
        private const string EXTRACT_COLLECTORS_CALLS_FILE_NAME = "collectors.calls.json";
        private const string EXTRACT_COLLECTORS_TIME_SPENT_FILE_NAME = "collectors.timespent.json";
        private const string EXTRACT_ALL_WAIT_STATES_FILE_NAME = "waitstatesall.json";

        // DB data file anmes
        private const string EXTRACT_CURRENT_WAIT_STATES_FILE_NAME = "waitstates.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.json";
        private const string EXTRACT_QUERIES_FILE_NAME = "queries.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.json";
        private const string EXTRACT_CLIENTS_FILE_NAME = "clients.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.json";
        private const string EXTRACT_SESSIONS_FILE_NAME = "sessions.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.json";
        private const string EXTRACT_BLOCKING_SESSIONS_FILE_NAME = "blockedsessions.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.json";
        private const string EXTRACT_BLOCKED_SESSION_FILE_NAME = "blockedsession.{0}.{1:yyyyMMddHHmm}-{2:yyyyMMddHHmm}.json";
        private const string EXTRACT_DATABASES_FILE_NAME = "databases.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.json";
        private const string EXTRACT_DB_USERS_FILE_NAME = "users.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.json";
        private const string EXTRACT_MODULES_FILE_NAME = "modules.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.json";
        private const string EXTRACT_PROGRAMS_FILE_NAME = "programs.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.json";
        private const string EXTRACT_BUSINESS_TRANSACTIONS_FILE_NAME = "bts.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.json";

        // RBAC data file names
        private const string EXTRACT_USERS_FILE_NAME = "users.json";
        private const string EXTRACT_GROUPS_FILE_NAME = "groups.json";
        private const string EXTRACT_ROLES_FILE_NAME = "roles.json";
        private const string EXTRACT_USER_FILE_NAME = "user.{0}.json";
        private const string EXTRACT_GROUP_FILE_NAME = "group.{0}.json";
        private const string EXTRACT_GROUP_USERS_FILE_NAME = "usersingroup.{0}.json";
        private const string EXTRACT_ROLE_FILE_NAME = "role.{0}.json";
        private const string EXTRACT_SECURITY_PROVIDER_FILE_NAME = "securityprovider.json";
        private const string EXTRACT_STRONG_PASSWORDS_FILE_NAME = "strongpasswords.json";

        // Metric data file names
        private const string EXTRACT_METRIC_FULL_FILE_NAME = "full.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.json";
        private const string EXTRACT_METRIC_HOUR_FILE_NAME = "hour.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.json";

        // Events data file names
        private const string HEALTH_RULE_VIOLATIONS_FILE_NAME = "healthruleviolations.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.json";
        private const string EVENTS_FILE_NAME = "{0}.{1:yyyyMMddHHmm}-{2:yyyyMMddHHmm}.json";
        private const string AUDIT_EVENTS_FILE_NAME = "auditevents.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.json";
        private const string NOTIFICATIONS_FILE_NAME = "notifications.json";

        // SIM Service Availability events data file names
        private const string SERVICE_AVAILABILITY_EVENTS_FILE_NAME = "saevents.{0}.{1:yyyyMMddHHmm}-{2:yyyyMMddHHmm}.json";

        // List of Snapshots file names
        private const string EXTRACT_SNAPSHOTS_FILE_NAME = "snapshots.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.json";

        // Snapshot file names
        private const string EXTRACT_SNAPSHOT_FILE_NAME = "{0}.{1}.{2:yyyyMMddHHmmss}.json";

        // Flowmap file names
        private const string EXTRACT_ENTITY_FLOWMAP_FILE_NAME = "flowmap.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.json";

        #endregion

        #region Constants for the folder and file names of data index

        private const string INDEX_FOLDER_NAME = "Index";

        // Detected entity report conversion file names
        private const string CONVERT_ENTITY_CONTROLLER_FILE_NAME = "controller.csv";
        private const string CONVERT_ENTITY_CONTROLLERS_FILE_NAME = "controllers.csv";
        private const string CONVERT_ENTITY_APPLICATIONS_FILE_NAME = "applications.csv";
        private const string CONVERT_ENTITY_APPLICATION_FILE_NAME = "application.csv";
        private const string CONVERT_ENTITY_TIERS_FILE_NAME = "tiers.csv";
        private const string CONVERT_ENTITY_NODES_FILE_NAME = "nodes.csv";
        private const string CONVERT_ENTITY_NODE_STARTUP_OPTIONS_FILE_NAME = "nodestartupoptions.csv";
        private const string CONVERT_ENTITY_NODE_PROPERTIES_FILE_NAME = "nodeproperties.csv";
        private const string CONVERT_ENTITY_NODE_ENVIRONMENT_VARIABLES_FILE_NAME = "nodeenvironmentvariables.csv";
        private const string CONVERT_ENTITY_BUSINESS_TRANSACTIONS_FILE_NAME = "businesstransactions.csv";
        private const string CONVERT_ENTITY_BACKENDS_FILE_NAME = "backends.csv";
        private const string CONVERT_ENTITY_SERVICE_ENDPOINTS_FILE_NAME = "serviceendpoints.csv";
        private const string CONVERT_ENTITY_ERRORS_FILE_NAME = "errors.csv";
        private const string CONVERT_ENTITY_INFORMATION_POINTS_FILE_NAME = "informationpoints.csv";
        private const string CONVERT_ENTITY_MAPPED_BACKENDS_FILE_NAME = "mappedbackends.csv";

        // Detected SIM entity report conversion file names
        private const string CONVERT_SIM_MACHINES_FILE_NAME = "machines.csv";
        private const string CONVERT_SIM_MACHINE_PROPERTIES_FILE_NAME = "machineproperties.csv";
        private const string CONVERT_SIM_MACHINE_CPUS_FILE_NAME = "machinecpus.csv";
        private const string CONVERT_SIM_MACHINE_VOLUMES_FILE_NAME = "machinevolumes.csv";
        private const string CONVERT_SIM_MACHINE_NETWORKS_FILE_NAME = "machinenetworks.csv";
        private const string CONVERT_SIM_MACHINE_CONTAINERS_FILE_NAME = "machinecontainers.csv";
        private const string CONVERT_SIM_MACHINE_PROCESSES_FILE_NAME = "machineprocesses.csv";

        // DB entity report conversion file names
        private const string CONVERT_DB_COLLECTOR_DEFINITIONS_FILE_NAME = "collectordefinitions.csv";
        private const string CONVERT_DB_COLLECTORS_FILE_NAME = "collectors.csv";
        private const string CONVERT_DB_WAIT_STATES_FILE_NAME = "waitstates.csv";
        private const string CONVERT_DB_QUERIES_FILE_NAME = "queries.csv";
        private const string CONVERT_DB_CLIENTS_FILE_NAME = "clients.csv";
        private const string CONVERT_DB_SESSIONS_FILE_NAME = "sessions.csv";
        private const string CONVERT_DB_BLOCKING_SESSIONS_FILE_NAME = "blockingsessions.csv";
        private const string CONVERT_DB_DATABASES_FILE_NAME = "databases.csv";
        private const string CONVERT_DB_USERS_FILE_NAME = "users.csv";
        private const string CONVERT_DB_MODULES_FILE_NAME = "modules.csv";
        private const string CONVERT_DB_PROGRAMS_FILE_NAME = "programs.csv";
        private const string CONVERT_DB_BUSINESS_TRANSACTIONS_FILE_NAME = "businesstransactions.csv";

        // RBAC report conversion file names
        private const string CONVERT_USERS_FILE_NAME = "users.csv";
        private const string CONVERT_GROUPS_FILE_NAME = "groups.csv";
        private const string CONVERT_ROLES_FILE_NAME = "roles.csv";
        private const string CONVERT_PERMISSIONS_FILE_NAME = "permissions.csv";
        private const string CONVERT_GROUP_MEMBERSHIPS_FILE_NAME = "groupmemberships.csv";
        private const string CONVERT_ROLE_MEMBERSHIPS_FILE_NAME = "rolememberships.csv";
        private const string CONVERT_USER_PERMISSIONS_FILE_NAME = "userpermissions.csv";
        private const string CONVERT_CONTROLLER_RBAC_SUMMARY_FILE_NAME = "controller.rbac.csv";

        // Settings report list conversion file names
        private const string CONTROLLER_SETTINGS_FILE_NAME = "controller.settings.csv";
        private const string APPLICATION_CONFIGURATION_FILE_NAME = "application.configuration.csv";
        private const string APPLICATION_CONFIGURATION_BUSINESS_TRANSACTION_DISCOVERY_RULES_FILE_NAME = "btdiscovery.rules.csv";
        private const string APPLICATION_CONFIGURATION_BUSINESS_TRANSACTION_DISCOVERY_RULES_2_0_FILE_NAME = "btdiscovery.rules.2.0.csv";
        private const string APPLICATION_CONFIGURATION_BUSINESS_TRANSACTION_ENTRY_RULES_FILE_NAME = "btentry.rules.csv";
        private const string APPLICATION_CONFIGURATION_BUSINESS_TRANSACTION_ENTRY_RULES_2_0_FILE_NAME = "btentry.rules.2.0.csv";
        private const string APPLICATION_CONFIGURATION_BUSINESS_TRANSACTION_ENTRY_SCOPES_FILE_NAME = "btentry.scopes.csv";
        private const string APPLICATION_CONFIGURATION_SERVICE_ENDPOINT_ENTRY_RULES_FILE_NAME = "sep.rules.csv";
        private const string APPLICATION_CONFIGURATION_BACKEND_DISCOVERY_RULES_FILE_NAME = "backend.rules.csv";
        private const string APPLICATION_CONFIGURATION_CUSTOM_EXIT_RULES_FILE_NAME = "customexit.rules.csv";
        private const string APPLICATION_CONFIGURATION_INFORMATION_POINT_RULES_FILE_NAME = "infopoints.csv";
        private const string APPLICATION_CONFIGURATION_AGENT_CONFIGURATION_PROPERTIES_FILE_NAME = "agent.properties.csv";
        private const string APPLICATION_CONFIGURATION_METHOD_INVOCATION_DATA_COLLECTORS_FILE_NAME = "datacollectors.midc.csv";
        private const string APPLICATION_CONFIGURATION_HTTP_DATA_COLLECTORS_FILE_NAME = "datacollectors.http.csv";
        private const string APPLICATION_CONFIGURATION_ENTITY_TIERS_FILE_NAME = "tiers.configuration.csv";
        private const string APPLICATION_CONFIGURATION_ENTITY_BUSINESS_TRANSACTIONS_FILE_NAME = "bts.configuration.csv";
        private const string APPLICATION_CONFIGURATION_AGENT_CALL_GRAPH_SETTINGS_FILE_NAME = "callgraphs.configuration.csv";
        private const string APPLICATION_CONFIGURATION_HEALTH_RULES_FILE_NAME = "healthrules.csv";
        private const string APPLICATION_CONFIGURATION_DEVELOPER_MODE_NODES_FILE_NAME = "devmodenodes.csv";

        // Configuration comparison report list conversion file names
        private const string CONFIGURATION_DIFFERENCES_FILE_NAME = "configuration.differences.csv";

        // Metric report conversion file names
        private const string CONVERT_ENTITIES_METRICS_SUMMARY_FULLRANGE_FILE_NAME = "entities.full.csv";
        private const string CONVERT_ENTITIES_METRICS_SUMMARY_HOURLY_FILE_NAME = "entities.hour.csv";
        private const string CONVERT_ENTITIES_ALL_METRICS_SUMMARY_FULLRANGE_FILE_NAME = "{0}.entities.full.csv";
        private const string CONVERT_ENTITIES_ALL_METRICS_SUMMARY_HOURLY_FILE_NAME = "{0}.entities.hour.csv";
        private const string CONVERT_ENTITIES_METRICS_VALUES_FILE_NAME = "{0}.metricvalues.csv";
        private const string CONVERT_ENTITIES_ALL_METRICS_VALUES_FILE_NAME = "{0}.{1}.metricvalues.csv";
        private const string CONVERT_ENTITIES_METRICS_LOCATIONS_FILE_NAME = "metriclocations.csv";

        // Events list conversion file names
        private const string CONVERT_APPLICATION_EVENTS_FILE_NAME = "application.events.csv";
        private const string CONVERT_EVENTS_FILE_NAME = "events.csv";
        private const string CONVERT_HEALTH_RULE_EVENTS_FILE_NAME = "hrviolationevents.csv";
        private const string CONVERT_AUDIT_EVENTS_FILE_NAME = "auditevents.csv";

        // Snapshots files
        private const string CONVERT_APPLICATION_SNAPSHOTS_FILE_NAME = "application.snapshots.csv";
        private const string CONVERT_SNAPSHOTS_FILE_NAME = "snapshots.csv";
        private const string CONVERT_SNAPSHOTS_SEGMENTS_FILE_NAME = "snapshots.segments.csv";
        private const string CONVERT_SNAPSHOTS_SEGMENTS_EXIT_CALLS_FILE_NAME = "snapshots.exits.csv";
        private const string CONVERT_SNAPSHOTS_SEGMENTS_SERVICE_ENDPOINTS_CALLS_FILE_NAME = "snapshots.serviceendpoints.csv";
        private const string CONVERT_SNAPSHOTS_SEGMENTS_DETECTED_ERRORS_FILE_NAME = "snapshots.errors.csv";
        private const string CONVERT_SNAPSHOTS_SEGMENTS_BUSINESS_DATA_FILE_NAME = "snapshots.businessdata.csv";
        private const string CONVERT_SNAPSHOTS_SEGMENTS_METHOD_CALL_LINES_FILE_NAME = "snapshots.methodcalllines.csv";
        private const string CONVERT_SNAPSHOTS_SEGMENTS_METHOD_CALL_LINES_OCCURRENCES_FILE_NAME = "snapshots.methodcalllinesoccurrences.csv";

        // Folded call stacks rollups
        private const string CONVERT_SNAPSHOTS_SEGMENTS_FOLDED_CALL_STACKS_FILE_NAME = "snapshots.foldedcallstacks.csv";
        private const string CONVERT_SNAPSHOTS_SEGMENTS_FOLDED_CALL_STACKS_WITH_TIME_FILE_NAME = "snapshots.foldedcallstackswithtime.csv";

        // Snapshots files for each BT and time ranges
        private const string CONVERT_SNAPSHOTS_TIMERANGE_FILE_NAME = "snapshots.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.csv";
        private const string CONVERT_SNAPSHOTS_SEGMENTS_TIMERANGE_FILE_NAME = "snapshots.segments.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.csv";
        private const string CONVERT_SNAPSHOTS_SEGMENTS_EXIT_CALLS_TIMERANGE_FILE_NAME = "snapshots.exits.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.csv";
        private const string CONVERT_SNAPSHOTS_SEGMENTS_SERVICE_ENDPOINTS_CALLS_TIMERANGE_FILE_NAME = "snapshots.serviceendpoints.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.csv";
        private const string CONVERT_SNAPSHOTS_SEGMENTS_DETECTED_ERRORS_TIMERANGE_FILE_NAME = "snapshots.errors.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.csv";
        private const string CONVERT_SNAPSHOTS_SEGMENTS_BUSINESS_DATA_TIMERANGE_FILE_NAME = "snapshots.businessdata.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.csv";
        private const string CONVERT_SNAPSHOTS_SEGMENTS_METHOD_CALL_LINES_FILE_TIMERANGE_NAME = "snapshots.methodcalllines.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.csv";
        private const string CONVERT_SNAPSHOTS_SEGMENTS_METHOD_CALL_LINES_OCCURRENCES_TIMERANGE_FILE_NAME = "snapshots.methodcalllinesoccurrences.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.csv";

        // Folded call stacks rollups for each BT and Nodes
        private const string CONVERT_SNAPSHOTS_SEGMENTS_FOLDED_CALL_STACKS_TIMERANGE_FILE_NAME = "snapshots.foldedcallstacks.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.csv";
        private const string CONVERT_SNAPSHOTS_SEGMENTS_FOLDED_CALL_STACKS_WITH_TIME_TIMERANGE_FILE_NAME = "snapshots.foldedcallstackswithtime.{0:yyyyMMddHHmm}-{1:yyyyMMddHHmm}.csv";

        // Snapshot files
        private const string CONVERT_SNAPSHOT_FILE_NAME = "snapshot.csv";
        private const string CONVERT_SNAPSHOT_SEGMENTS_FILE_NAME = "snapshot.segments.csv";
        private const string CONVERT_SNAPSHOT_SEGMENTS_EXIT_CALLS_FILE_NAME = "snapshot.exits.csv";
        private const string CONVERT_SNAPSHOT_SEGMENTS_SERVICE_ENDPOINTS_CALLS_FILE_NAME = "snapshot.serviceendpoints.csv";
        private const string CONVERT_SNAPSHOT_SEGMENTS_DETECTED_ERRORS_FILE_NAME = "snapshot.errors.csv";
        private const string CONVERT_SNAPSHOT_SEGMENTS_BUSINESS_DATA_FILE_NAME = "snapshot.businessdata.csv";
        private const string CONVERT_SNAPSHOT_SEGMENTS_METHOD_CALL_LINES_FILE_NAME = "snapshot.methodcalllines.csv";
        private const string CONVERT_SNAPSHOT_SEGMENTS_METHOD_CALL_LINES_OCCURRENCES_FILE_NAME = "snapshot.methodcalllinesoccurrences.csv";

        // Folded call stacks for snapshot
        private const string CONVERT_SNAPSHOT_SEGMENTS_FOLDED_CALL_STACKS_FILE_NAME = "snapshot.foldedcallstacks.csv";
        private const string CONVERT_SNAPSHOT_SEGMENTS_FOLDED_CALL_STACKS_WITH_TIME_FILE_NAME = "snapshot.foldedcallstacks.withtime.csv";

        // Flow map to flow grid conversion file names
        private const string CONVERT_ACTIVITY_GRIDS_FILE_NAME = "activitygrids.full.csv";
        private const string CONVERT_ALL_ACTIVITY_GRIDS_FILE_NAME = "{0}.activitygrids.full.csv";
        private const string CONVERT_ACTIVITY_GRIDS_PERMINUTE_FILE_NAME = "activitygrids.perminute.full.csv";
        private const string CONVERT_ALL_ACTIVITY_GRIDS_PERMINUTE_FILE_NAME = "{0}.activitygrids.perminute.full.csv";

        #endregion

        #region Constants for the folder and file names of data reports

        private const string REPORT_FOLDER_NAME = "Report";

        // Report file names
        private const string REPORT_DETECTED_ENTITIES_FILE_NAME = "DetectedEntities.{0}.{1:yyyyMMddHHmm}-{2:yyyyMMddHHmm}.xlsx";
        private const string REPORT_DETECTED_SIM_ENTITIES_FILE_NAME = "DetectedEntities.SIM.{0}.{1:yyyyMMddHHmm}-{2:yyyyMMddHHmm}.xlsx";
        private const string REPORT_DETECTED_DB_ENTITIES_FILE_NAME = "DetectedEntities.DB.{0}.{1:yyyyMMddHHmm}-{2:yyyyMMddHHmm}.xlsx";
        private const string REPORT_METRICS_ALL_ENTITIES_FILE_NAME = "EntityMetrics.{0}.{1:yyyyMMddHHmm}-{2:yyyyMMddHHmm}.xlsx";
        private const string REPORT_DETECTED_EVENTS_FILE_NAME = "Events.{0}.{1:yyyyMMddHHmm}-{2:yyyyMMddHHmm}.xlsx";
        private const string REPORT_SNAPSHOTS_FILE_NAME = "Snapshots.{0}.{1:yyyyMMddHHmm}-{2:yyyyMMddHHmm}.xlsx";
        private const string REPORT_SNAPSHOTS_METHOD_CALL_LINES_FILE_NAME = "Snapshots.MethodCallLines.{0}.{1:yyyyMMddHHmm}-{2:yyyyMMddHHmm}.xlsx";
        private const string REPORT_CONFIGURATION_FILE_NAME = "Configuration.{0}.{1:yyyyMMddHHmm}-{2:yyyyMMddHHmm}.xlsx";
        private const string REPORT_USERS_GROUPS_ROLES_PERMISSIONS_FILE_NAME = "UsersGroupsRoles.{0}.{1:yyyyMMddHHmm}-{2:yyyyMMddHHmm}.xlsx";

        // Per entity report names
        private const string REPORT_ENTITY_DETAILS_APPLICATION_FILE_NAME = "EntityDetails.{0}.{1}.{2:yyyyMMddHHmm}-{3:yyyyMMddHHmm}.xlsx";
        private const string REPORT_ENTITY_DETAILS_ENTITY_FILE_NAME = "EntityDetails.{0}.{1}.{2}.{3:yyyyMMddHHmm}-{4:yyyyMMddHHmm}.xlsx";
        private const string REPORT_METRICS_GRAPHS_FILE_NAME = "MetricGraphs.{0}.{1}.{2}.{3:yyyyMMddHHmm}-{4:yyyyMMddHHmm}.xlsx";

        // Per entity flame graph report name
        private const string REPORT_FLAME_GRAPH_APPLICATION_FILE_NAME = "FlameGraph.Application.{0}.{1}.{2:yyyyMMddHHmm}-{3:yyyyMMddHHmm}.svg";
        private const string REPORT_FLAME_GRAPH_TIER_FILE_NAME = "FlameGraph.Tier.{0}.{1}.{2}.{3:yyyyMMddHHmm}-{4:yyyyMMddHHmm}.svg";
        private const string REPORT_FLAME_GRAPH_NODE_FILE_NAME = "FlameGraph.Node.{0}.{1}.{2}.{3:yyyyMMddHHmm}-{4:yyyyMMddHHmm}.svg";
        private const string REPORT_FLAME_GRAPH_BUSINESS_TRANSACTION_FILE_NAME = "FlameGraph.BT.{0}.{1}.{2}.{3:yyyyMMddHHmm}-{4:yyyyMMddHHmm}.svg";
        private const string REPORT_FLAME_GRAPH_SNAPSHOT_FILE_NAME = "FlameGraph.Snapshot.{0}.{1:yyyyMMddHHmmss}.{2}.svg";

        // Per entity flame chart report name
        private const string REPORT_FLAME_CHART_APPLICATION_FILE_NAME = "FlameChart.Application.{0}.{1}.{2:yyyyMMddHHmm}-{3:yyyyMMddHHmm}.svg";
        private const string REPORT_FLAME_CHART_TIER_FILE_NAME = "FlameChart.Tier.{0}.{1}.{2}.{3:yyyyMMddHHmm}-{4:yyyyMMddHHmm}.svg";
        private const string REPORT_FLAME_CHART_NODE_FILE_NAME = "FlameChart.Node.{0}.{1}.{2}.{3:yyyyMMddHHmm}-{4:yyyyMMddHHmm}.svg";
        private const string REPORT_FLAME_CHART_BUSINESS_TRANSACTION_FILE_NAME = "FlameChart.BT.{0}.{1}.{2}.{3:yyyyMMddHHmm}-{4:yyyyMMddHHmm}.svg";

        #endregion

        #region Constants for Step Timing report

        private const string TIMING_REPORT_FILE_NAME = "StepDurations.csv";

        #endregion

        #region Constants for lookup and external files

        // Settings for method and call mapping
        private const string METHOD_CALL_LINES_TO_FRAMEWORK_TYPE_MAPPING_FILE_NAME = "MethodNamespaceTypeMapping.csv";

        // Settings for the metric extracts
        private const string ENTITY_METRICS_EXTRACT_MAPPING_FILE_NAME = "EntityMetricsExtractMapping.csv";

        // Flame graph template SVG XML file
        private const string FLAME_GRAPH_TEMPLATE_FILE_NAME = "FlameGraphTemplate.svg";

        // Template application export of an empty application
        private const string TEMPLATE_APPLICATION_CONFIGURATION_FILE_NAME = "TemplateApplicationConfiguration.xml";


        #endregion

        #region Snapshot UX to Folder Mapping

        internal static Dictionary<string, string> USEREXPERIENCE_FOLDER_MAPPING = new Dictionary<string, string>
        {
            {"NORMAL", "NM"},
            {"SLOW", "SL"},
            {"VERY_SLOW", "VS"},
            {"STALL", "ST"},
            {"ERROR", "ER"}
        };

        #endregion

        #region Constructor and properties

        public ProgramOptions ProgramOptions { get; set; }

        public JobConfiguration JobConfiguration { get; set; }

        public FilePathMap(ProgramOptions programOptions, JobConfiguration jobConfiguration)
        {
            this.ProgramOptions = programOptions;
            this.JobConfiguration = jobConfiguration;
        }

        #endregion


        #region Step Timing Report

        public string StepTimingReportFilePath()
        {
            return Path.Combine(this.ProgramOptions.OutputJobFolderPath, REPORT_FOLDER_NAME, TIMING_REPORT_FILE_NAME);
        }

        #endregion


        #region Entity Metadata Data

        public string ControllerVersionDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                EXTRACT_CONTROLLER_VERSION_FILE_NAME);
        }

        public string ApplicationsDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                EXTRACT_ENTITY_APPLICATIONS_FILE_NAME);
        }

        public string ApplicationDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                EXTRACT_ENTITY_APPLICATION_FILE_NAME);
        }

        public string TiersDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                EXTRACT_ENTITY_TIERS_FILE_NAME);
        }

        public string NodesDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                EXTRACT_ENTITY_NODES_FILE_NAME);
        }

        public string BackendsDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                EXTRACT_ENTITY_BACKENDS_FILE_NAME);
        }

        public string BackendsDetailDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                EXTRACT_ENTITY_BACKENDS_DETAIL_FILE_NAME);
        }

        public string BackendToTierMappingDataFilePath(JobTarget jobTarget, AppDRESTTier tier)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(tier.name, tier.id),
                EXTRACT_ENTITY_BACKEND_TO_TIER_MAPPING_FILE_NAME);
        }

        public string BusinessTransactionsDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                EXTRACT_ENTITY_BUSINESS_TRANSACTIONS_FILE_NAME);
        }

        public string ServiceEndpointsDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                EXTRACT_ENTITY_SERVICE_ENDPOINTS_FILE_NAME);
        }

        public string ServiceEndpointsDetailDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                EXTRACT_ENTITY_SERVICE_ENDPOINTS_DETAIL_FILE_NAME);
        }

        public string ErrorsDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                EXTRACT_ENTITY_ERRORS_FILE_NAME);
        }

        public string InformationPointsDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                EXTRACT_ENTITY_INFORMATION_POINTS_FILE_NAME);
        }

        public string InformationPointsDetailDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                EXTRACT_ENTITY_INFORMATION_POINTS_DETAIL_FILE_NAME);
        }

        public string NodeRuntimePropertiesDataFilePath(JobTarget jobTarget, AppDRESTNode node)
        {
            string reportFileName = String.Format(
                EXTRACT_ENTITY_NODE_RUNTIME_PROPERTIES_FILE_NAME,
                getShortenedEntityNameForFileSystem(node.name, node.id));

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(node.tierName, node.tierId),
                reportFileName);
        }

        public string NodeMetadataDataFilePath(JobTarget jobTarget, AppDRESTNode node)
        {
            string reportFileName = String.Format(
                EXTRACT_ENTITY_NODE_METADATA_FILE_NAME,
                getShortenedEntityNameForFileSystem(node.name, node.id));

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(node.tierName, node.tierId),
                reportFileName);
        }

        public string BackendToDBMonMappingDataFilePath(JobTarget jobTarget, AppDRESTBackend backend)
        {
            string reportFileName = String.Format(
                EXTRACT_ENTITY_BACKEND_TO_DBMON_MAPPING_FILE_NAME,
                getShortenedEntityNameForFileSystem(backend.name, backend.id));

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                reportFileName);
        }

        #endregion

        #region Entity Metadata Index

        public string ControllerIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                CONVERT_ENTITY_CONTROLLER_FILE_NAME);
        }

        public string ApplicationsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                CONVERT_ENTITY_APPLICATIONS_FILE_NAME);
        }

        public string ApplicationIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONVERT_ENTITY_APPLICATION_FILE_NAME);
        }

        public string TiersIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_TIERS_FILE_NAME);
        }

        public string NodesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_NODES_FILE_NAME);
        }

        public string NodeStartupOptionsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_NODE_STARTUP_OPTIONS_FILE_NAME);
        }

        public string NodePropertiesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_NODE_PROPERTIES_FILE_NAME);
        }

        public string NodeEnvironmentVariablesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_NODE_ENVIRONMENT_VARIABLES_FILE_NAME);
        }

        public string BackendsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_BACKENDS_FILE_NAME);
        }

        public string MappedBackendsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_MAPPED_BACKENDS_FILE_NAME);
        }

        public string BusinessTransactionsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_BUSINESS_TRANSACTIONS_FILE_NAME);
        }

        public string ServiceEndpointsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_SERVICE_ENDPOINTS_FILE_NAME);
        }

        public string ErrorsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_ERRORS_FILE_NAME);
        }

        public string InformationPointsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_INFORMATION_POINTS_FILE_NAME);
        }

        #endregion

        #region Entity Metadata Report

        public string ReportFolderPath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME);
        }

        public string EntitiesReportFolderPath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                ENTITIES_FOLDER_NAME);
        }

        public string ControllersReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_CONTROLLERS_FILE_NAME);
        }

        public string ApplicationsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_APPLICATIONS_FILE_NAME);
        }

        public string TiersReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_TIERS_FILE_NAME);
        }

        public string NodesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_NODES_FILE_NAME);
        }

        public string NodeStartupOptionsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_NODE_STARTUP_OPTIONS_FILE_NAME);
        }

        public string NodePropertiesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_NODE_PROPERTIES_FILE_NAME);
        }

        public string NodeEnvironmentVariablesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_NODE_ENVIRONMENT_VARIABLES_FILE_NAME);
        }

        public string BackendsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_BACKENDS_FILE_NAME);
        }

        public string MappedBackendsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_MAPPED_BACKENDS_FILE_NAME);
        }

        public string BusinessTransactionsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_BUSINESS_TRANSACTIONS_FILE_NAME);
        }

        public string ServiceEndpointsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_SERVICE_ENDPOINTS_FILE_NAME);
        }

        public string ErrorsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_ERRORS_FILE_NAME);
        }

        public string InformationPointsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_INFORMATION_POINTS_FILE_NAME);
        }

        public string EntitiesExcelReportFilePath(JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                REPORT_DETECTED_ENTITIES_FILE_NAME,
                this.ProgramOptions.JobName,
                jobTimeRange.From,
                jobTimeRange.To);
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                reportFileName);
        }

        #endregion


        #region SIM Entity Metadata Data

        public string SIMTiersDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SIM_ENTITIES_FOLDER_NAME,
                EXTRACT_ENTITY_TIERS_FILE_NAME);
        }

        public string SIMNodesDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SIM_ENTITIES_FOLDER_NAME,
                EXTRACT_ENTITY_NODES_FILE_NAME);
        }

        public string SIMGroupsDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SIM_ENTITIES_FOLDER_NAME,
                EXTRACT_ENTITY_GROUPS_FILE_NAME);
        }

        public string SIMMachinesDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SIM_ENTITIES_FOLDER_NAME,
                EXTRACT_ENTITY_MACHINES_FILE_NAME);
        }

        public string SIMMachineDataFilePath(JobTarget jobTarget, string machineName, long machineID)
        {
            string reportFileName = String.Format(
                EXTRACT_ENTITY_MACHINE_FILE_NAME,
                getShortenedEntityNameForFileSystem(machineName, machineID));

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SIM_ENTITIES_FOLDER_NAME,
                reportFileName);
        }

        public string SIMMachineDockerContainersDataFilePath(JobTarget jobTarget, string machineName, long machineID)
        {
            string reportFileName = String.Format(
                EXTRACT_ENTITY_DOCKER_CONTAINERS_FILE_NAME,
                getShortenedEntityNameForFileSystem(machineName, machineID));

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SIM_ENTITIES_FOLDER_NAME,
                reportFileName);
        }

        public string SIMServiceAvailabilitiesDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SIM_ENTITIES_FOLDER_NAME,
                EXTRACT_ENTITY_SERVICE_AVAILABILITIES_FILE_NAME);
        }

        public string SIMServiceAvailabilityDataFilePath(JobTarget jobTarget, string saName, long saID)
        {
            string reportFileName = String.Format(
                EXTRACT_ENTITY_SERVICE_AVAILABILITY_FILE_NAME,
                getShortenedEntityNameForFileSystem(saName, saID));

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SIM_ENTITIES_FOLDER_NAME,
                reportFileName);
        }

        public string SIMMachineProcessesDataFilePath(JobTarget jobTarget, string machineName, long machineID, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                EXTRACT_PROCESSES_FILE_NAME,
                getShortenedEntityNameForFileSystem(machineName, machineID),
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                PROCESSES_FOLDER_NAME,
                reportFileName);
        }

        public string SIMServiceAvailabilityEventsDataFilePath(JobTarget jobTarget, string saName, long saID, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                SERVICE_AVAILABILITY_EVENTS_FILE_NAME,
                getShortenedEntityNameForFileSystem(saName, saID),
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SA_EVENTS_FOLDER_NAME,
                reportFileName);
        }

        #endregion

        #region SIM Entity Metadata Index

        public string SIMApplicationIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SIM_ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_APPLICATION_FILE_NAME);
        }

        public string SIMTiersIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SIM_ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_TIERS_FILE_NAME);
        }

        public string SIMNodesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SIM_ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_NODES_FILE_NAME);
        }

        public string SIMMachinesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SIM_ENTITIES_FOLDER_NAME,
                CONVERT_SIM_MACHINES_FILE_NAME);
        }

        public string SIMMachinePropertiesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SIM_ENTITIES_FOLDER_NAME,
                CONVERT_SIM_MACHINE_PROPERTIES_FILE_NAME);
        }

        public string SIMMachineCPUsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SIM_ENTITIES_FOLDER_NAME,
                CONVERT_SIM_MACHINE_CPUS_FILE_NAME);
        }

        public string SIMMachineVolumesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SIM_ENTITIES_FOLDER_NAME,
                CONVERT_SIM_MACHINE_VOLUMES_FILE_NAME);
        }

        public string SIMMachineNetworksIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SIM_ENTITIES_FOLDER_NAME,
                CONVERT_SIM_MACHINE_NETWORKS_FILE_NAME);
        }

        public string SIMMachineContainersIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SIM_ENTITIES_FOLDER_NAME,
                CONVERT_SIM_MACHINE_CONTAINERS_FILE_NAME);
        }

        public string SIMMachineProcessesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                PROCESSES_FOLDER_NAME,
                CONVERT_SIM_MACHINE_PROCESSES_FILE_NAME);
        }

        #endregion

        #region SIM Entity Metadata Report

        public string SIMEntitiesReportFolderPath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                SIM_ENTITIES_FOLDER_NAME);
        }

        public string SIMApplicationsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                SIM_ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_APPLICATIONS_FILE_NAME);
        }

        public string SIMTiersReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                SIM_ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_TIERS_FILE_NAME);
        }

        public string SIMNodesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                SIM_ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_NODES_FILE_NAME);
        }

        public string SIMMachinesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                SIM_ENTITIES_FOLDER_NAME,
                CONVERT_SIM_MACHINES_FILE_NAME);
        }

        public string SIMMachinePropertiesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                SIM_ENTITIES_FOLDER_NAME,
                CONVERT_SIM_MACHINE_PROPERTIES_FILE_NAME);
        }

        public string SIMMachineCPUsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                SIM_ENTITIES_FOLDER_NAME,
                CONVERT_SIM_MACHINE_CPUS_FILE_NAME);
        }

        public string SIMMachineVolumesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                SIM_ENTITIES_FOLDER_NAME,
                CONVERT_SIM_MACHINE_VOLUMES_FILE_NAME);
        }

        public string SIMMachineNetworksReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                SIM_ENTITIES_FOLDER_NAME,
                CONVERT_SIM_MACHINE_NETWORKS_FILE_NAME);
        }

        public string SIMMachineContainersReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                SIM_ENTITIES_FOLDER_NAME,
                CONVERT_SIM_MACHINE_CONTAINERS_FILE_NAME);
        }

        public string SIMMachineProcessesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                SIM_ENTITIES_FOLDER_NAME,
                CONVERT_SIM_MACHINE_PROCESSES_FILE_NAME);
        }

        public string SIMEntitiesExcelReportFilePath(JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                REPORT_DETECTED_SIM_ENTITIES_FILE_NAME,
                this.ProgramOptions.JobName,
                jobTimeRange.From,
                jobTimeRange.To);
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                reportFileName);
        }

        #endregion


        #region DB Entity Metadata and Data

        public string DBCollectorDefinitionsDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                DB_APPLICATION_FOLDER_NAME,
                DB_ENTITIES_FOLDER_NAME,
                EXTRACT_COLLECTOR_DEFINITIONS_FILE_NAME);
        }

        public string DBCollectorsCallsDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                DB_APPLICATION_FOLDER_NAME,
                DB_ENTITIES_FOLDER_NAME,
                EXTRACT_COLLECTORS_CALLS_FILE_NAME);
        }

        public string DBCollectorsTimeSpentDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                DB_APPLICATION_FOLDER_NAME,
                DB_ENTITIES_FOLDER_NAME,
                EXTRACT_COLLECTORS_TIME_SPENT_FILE_NAME);
        }

        public string DBAllWaitStatesDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_DATA_FOLDER_NAME,
                EXTRACT_ALL_WAIT_STATES_FILE_NAME);
        }

        public string DBCurrentWaitStatesDataFilePath(JobTarget jobTarget, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                EXTRACT_CURRENT_WAIT_STATES_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);
            
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_DATA_FOLDER_NAME,
                reportFileName);
        }

        public string DBQueriesDataFilePath(JobTarget jobTarget, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                EXTRACT_QUERIES_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_DATA_FOLDER_NAME,
                reportFileName);
        }

        public string DBClientsDataFilePath(JobTarget jobTarget, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                EXTRACT_CLIENTS_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_DATA_FOLDER_NAME,
                reportFileName);
        }

        public string DBSessionsDataFilePath(JobTarget jobTarget, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                EXTRACT_SESSIONS_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_DATA_FOLDER_NAME,
                reportFileName);
        }

        public string DBBlockingSessionsDataFilePath(JobTarget jobTarget, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                EXTRACT_BLOCKING_SESSIONS_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_DATA_FOLDER_NAME,
                reportFileName);
        }

        public string DBBlockingSessionDataFilePath(JobTarget jobTarget, JobTimeRange jobTimeRange, long sessionID)
        {
            string reportFileName = String.Format(
                EXTRACT_BLOCKED_SESSION_FILE_NAME,
                sessionID,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_DATA_FOLDER_NAME,
                reportFileName);
        }

        public string DBDatabasesDataFilePath(JobTarget jobTarget, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                EXTRACT_DATABASES_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_DATA_FOLDER_NAME,
                reportFileName);
        }

        public string DBUsersDataFilePath(JobTarget jobTarget, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                EXTRACT_DB_USERS_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_DATA_FOLDER_NAME,
                reportFileName);
        }

        public string DBModulesDataFilePath(JobTarget jobTarget, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                EXTRACT_MODULES_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_DATA_FOLDER_NAME,
                reportFileName);
        }

        public string DBProgramsDataFilePath(JobTarget jobTarget, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                EXTRACT_PROGRAMS_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_DATA_FOLDER_NAME,
                reportFileName);
        }

        public string DBBusinessTransactionsDataFilePath(JobTarget jobTarget, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                EXTRACT_BUSINESS_TRANSACTIONS_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_DATA_FOLDER_NAME,
                reportFileName);
        }

        #endregion

        #region DB Entity Metadata Index

        public string DBCollectorDefinitionsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                DB_APPLICATION_FOLDER_NAME,
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_COLLECTOR_DEFINITIONS_FILE_NAME);
        }

        public string DBCollectorsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                DB_APPLICATION_FOLDER_NAME,
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_COLLECTORS_FILE_NAME);
        }

        public string DBApplicationIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                DB_APPLICATION_FOLDER_NAME,
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_APPLICATION_FILE_NAME);
        }

        public string DBWaitStatesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_WAIT_STATES_FILE_NAME);
        }

        public string DBQueriesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_QUERIES_FILE_NAME);
        }

        public string DBClientsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_CLIENTS_FILE_NAME);
        }

        public string DBSessionsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_SESSIONS_FILE_NAME);
        }

        public string DBBlockingSessionsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_BLOCKING_SESSIONS_FILE_NAME);
        }

        public string DBDatabasesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_DATABASES_FILE_NAME);
        }

        public string DBUsersIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_USERS_FILE_NAME);
        }

        public string DBModulesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_MODULES_FILE_NAME);
        }

        public string DBProgramsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_PROGRAMS_FILE_NAME);
        }

        public string DBBusinessTransactionsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_BUSINESS_TRANSACTIONS_FILE_NAME);
        }
        
        #endregion

        #region DB Entity Metadata Report

        public string DBEntitiesReportFolderPath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                DB_ENTITIES_FOLDER_NAME);
        }

        public string DBCollectorDefinitionsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_COLLECTOR_DEFINITIONS_FILE_NAME);
        }

        public string DBCollectorsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_COLLECTORS_FILE_NAME);
        }

        public string DBApplicationsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_ENTITY_APPLICATIONS_FILE_NAME);
        }

        public string DBWaitStatesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_WAIT_STATES_FILE_NAME);
        }

        public string DBQueriesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_QUERIES_FILE_NAME);
        }

        public string DBClientsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_CLIENTS_FILE_NAME);
        }

        public string DBSessionsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_SESSIONS_FILE_NAME);
        }

        public string DBBlockingSessionsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_BLOCKING_SESSIONS_FILE_NAME);
        }

        public string DBDatabasesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_DATABASES_FILE_NAME);
        }

        public string DBUsersReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_USERS_FILE_NAME);
        }

        public string DBModulesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_MODULES_FILE_NAME);
        }

        public string DBProgramsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_PROGRAMS_FILE_NAME);
        }

        public string DBBusinessTransactionsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                DB_ENTITIES_FOLDER_NAME,
                CONVERT_DB_BUSINESS_TRANSACTIONS_FILE_NAME);
        }

        public string DBEntitiesExcelReportFilePath(JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                REPORT_DETECTED_DB_ENTITIES_FILE_NAME,
                this.ProgramOptions.JobName,
                jobTimeRange.From,
                jobTimeRange.To);
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                reportFileName);
        }

        #endregion


        #region Configuration Data

        public string ControllerSettingsDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                EXTRACT_CONFIGURATION_CONTROLLER_FILE_NAME);
        }

        public string ApplicationConfigurationDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_FOLDER_NAME,
                EXTRACT_CONFIGURATION_APPLICATION_FILE_NAME);
        }

        public string ApplicationConfigurationSEPDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_FOLDER_NAME,
                EXTRACT_CONFIGURATION_APPLICATION_SEP_FILE_NAME);
        }

        public string DeveloperModeNodesDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_FOLDER_NAME,
                EXTRACT_CONFIGURATION_DEVELOPER_MODE_NODES_FILE_NAME);
        }

        #endregion

        #region Configuration Index

        public string ControllerSettingsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                CONTROLLER_SETTINGS_FILE_NAME);
        }

        public string ApplicationConfigurationIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_FILE_NAME);
        }

        public string BusinessTransactionDiscoveryRulesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_BUSINESS_TRANSACTION_DISCOVERY_RULES_FILE_NAME);
        }

        public string BusinessTransactionEntryRulesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_BUSINESS_TRANSACTION_ENTRY_RULES_FILE_NAME);
        }

        public string ServiceEndpointEntryRulesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_SERVICE_ENDPOINT_ENTRY_RULES_FILE_NAME);
        }

        public string BusinessTransactionEntryScopesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_BUSINESS_TRANSACTION_ENTRY_SCOPES_FILE_NAME);
        }

        public string BusinessTransactionEntryRules20IndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_BUSINESS_TRANSACTION_ENTRY_RULES_2_0_FILE_NAME);
        }

        public string BusinessTransactionDiscoveryRules20IndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_BUSINESS_TRANSACTION_DISCOVERY_RULES_2_0_FILE_NAME);
        }

        public string BackendDiscoveryRulesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_BACKEND_DISCOVERY_RULES_FILE_NAME);
        }

        public string CustomExitRulesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_CUSTOM_EXIT_RULES_FILE_NAME);
        }

        public string AgentConfigurationPropertiesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_AGENT_CONFIGURATION_PROPERTIES_FILE_NAME);
        }

        public string InformationPointRulesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_INFORMATION_POINT_RULES_FILE_NAME);
        }

        public string EntityBusinessTransactionConfigurationsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_ENTITY_BUSINESS_TRANSACTIONS_FILE_NAME);
        }

        public string EntityTierConfigurationsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_ENTITY_TIERS_FILE_NAME);
        }

        public string MethodInvocationDataCollectorsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_METHOD_INVOCATION_DATA_COLLECTORS_FILE_NAME);
        }

        public string HttpDataCollectorsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_HTTP_DATA_COLLECTORS_FILE_NAME);
        }

        public string AgentCallGraphSettingsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_AGENT_CALL_GRAPH_SETTINGS_FILE_NAME);
        }

        public string HealthRulesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_HEALTH_RULES_FILE_NAME);
        }

        public string DeveloperModeNodesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_DEVELOPER_MODE_NODES_FILE_NAME);
        }

        #endregion

        #region Configuration Report

        public string ConfigurationReportFolderPath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_FOLDER_NAME);
        }

        public string ControllerSettingsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_FOLDER_NAME,
                CONTROLLER_SETTINGS_FILE_NAME);
        }

        public string ApplicationConfigurationReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_FILE_NAME);
        }

        public string BusinessTransactionDiscoveryRulesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_BUSINESS_TRANSACTION_DISCOVERY_RULES_FILE_NAME);
        }

        public string BusinessTransactionEntryRulesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_BUSINESS_TRANSACTION_ENTRY_RULES_FILE_NAME);
        }

        public string ServiceEndpointEntryRulesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_SERVICE_ENDPOINT_ENTRY_RULES_FILE_NAME);
        }

        public string BusinessTransactionEntryScopesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_BUSINESS_TRANSACTION_ENTRY_SCOPES_FILE_NAME);
        }

        public string BusinessTransactionEntryRules20ReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_BUSINESS_TRANSACTION_ENTRY_RULES_2_0_FILE_NAME);
        }

        public string BusinessTransactionDiscoveryRules20ReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_BUSINESS_TRANSACTION_DISCOVERY_RULES_2_0_FILE_NAME);
        }

        public string BackendDiscoveryRulesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_BACKEND_DISCOVERY_RULES_FILE_NAME);
        }

        public string CustomExitRulesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_CUSTOM_EXIT_RULES_FILE_NAME);
        }

        public string AgentConfigurationPropertiesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_AGENT_CONFIGURATION_PROPERTIES_FILE_NAME);
        }

        public string InformationPointRulesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_INFORMATION_POINT_RULES_FILE_NAME);
        }

        public string EntityBusinessTransactionConfigurationsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_ENTITY_BUSINESS_TRANSACTIONS_FILE_NAME);
        }

        public string EntityTierConfigurationsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_ENTITY_TIERS_FILE_NAME);
        }

        public string MethodInvocationDataCollectorsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_METHOD_INVOCATION_DATA_COLLECTORS_FILE_NAME);
        }

        public string HttpDataCollectorsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_HTTP_DATA_COLLECTORS_FILE_NAME);
        }

        public string AgentCallGraphSettingsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_AGENT_CALL_GRAPH_SETTINGS_FILE_NAME);
        }

        public string HealthRulesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_HEALTH_RULES_FILE_NAME);
        }

        public string DeveloperModeNodesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_FOLDER_NAME,
                APPLICATION_CONFIGURATION_DEVELOPER_MODE_NODES_FILE_NAME);
        }

        public string ConfigurationExcelReportFilePath(JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                REPORT_CONFIGURATION_FILE_NAME,
                this.ProgramOptions.JobName,
                jobTimeRange.From,
                jobTimeRange.To);
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                reportFileName);
        }

        #endregion


        #region Configuration Comparison Data

        public string TemplateApplicationConfigurationFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.ProgramLocationFolderPath,
                TEMPLATE_APPLICATION_CONFIGURATION_FILE_NAME);
        }

        #endregion

        #region Configuration Comparison Index

        public string ConfigurationComparisonIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                CONFIGURATION_COMPARISON_FOLDER_NAME,
                CONFIGURATION_DIFFERENCES_FILE_NAME);
        }

        #endregion

        #region Configuration Comparison Report

        public string ConfigurationComparisonReportFolderPath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_COMPARISON_FOLDER_NAME);
        }

        public string ConfigurationComparisonReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                CONFIGURATION_COMPARISON_FOLDER_NAME,
                CONFIGURATION_DIFFERENCES_FILE_NAME);
        }

        #endregion


        #region RBAC Data

        public string UsersDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                RBAC_REPOSITORY_FOLDER_NAME,
                RBAC_FOLDER_NAME,
                EXTRACT_USERS_FILE_NAME);
        }

        public string GroupsDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                RBAC_REPOSITORY_FOLDER_NAME,
                RBAC_FOLDER_NAME,
                EXTRACT_GROUPS_FILE_NAME);
        }

        public string RolesDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                RBAC_REPOSITORY_FOLDER_NAME,
                RBAC_FOLDER_NAME,
                EXTRACT_ROLES_FILE_NAME);
        }

        public string UserDataFilePath(JobTarget jobTarget, string userName, long userID)
        {
            string reportFileName = String.Format(
                EXTRACT_USER_FILE_NAME,
                getShortenedEntityNameForFileSystem(userName, userID));

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                RBAC_REPOSITORY_FOLDER_NAME,
                RBAC_FOLDER_NAME,
                reportFileName);
        }

        public string GroupDataFilePath(JobTarget jobTarget, string groupName, long groupID)
        {
            string reportFileName = String.Format(
                EXTRACT_GROUP_FILE_NAME,
                getShortenedEntityNameForFileSystem(groupName, groupID));

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                RBAC_REPOSITORY_FOLDER_NAME,
                RBAC_FOLDER_NAME,
                reportFileName);
        }

        public string GroupUsersDataFilePath(JobTarget jobTarget, string groupName, long groupID)
        {
            string reportFileName = String.Format(
                EXTRACT_GROUP_USERS_FILE_NAME,
                getShortenedEntityNameForFileSystem(groupName, groupID));

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                RBAC_REPOSITORY_FOLDER_NAME,
                RBAC_FOLDER_NAME,
                reportFileName);
        }


        public string RoleDataFilePath(JobTarget jobTarget, string roleName, long roleID)
        {
            string reportFileName = String.Format(
                EXTRACT_ROLE_FILE_NAME,
                getShortenedEntityNameForFileSystem(roleName, roleID));

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                RBAC_REPOSITORY_FOLDER_NAME,
                RBAC_FOLDER_NAME,
                reportFileName);
        }

        public string SecurityProviderTypeDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                RBAC_REPOSITORY_FOLDER_NAME,
                RBAC_FOLDER_NAME,
                EXTRACT_SECURITY_PROVIDER_FILE_NAME);
        }

        public string StrongPasswordsDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                RBAC_REPOSITORY_FOLDER_NAME,
                RBAC_FOLDER_NAME,
                EXTRACT_STRONG_PASSWORDS_FILE_NAME);
        }

        #endregion

        #region RBAC Index

        public string UsersIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                CONVERT_USERS_FILE_NAME);
        }

        public string GroupsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                CONVERT_GROUPS_FILE_NAME);
        }

        public string RolesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                CONVERT_ROLES_FILE_NAME);
        }

        public string PermissionsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                CONVERT_PERMISSIONS_FILE_NAME);
        }        

        public string GroupMembershipsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                CONVERT_GROUP_MEMBERSHIPS_FILE_NAME);
        }

        public string RoleMembershipsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                CONVERT_ROLE_MEMBERSHIPS_FILE_NAME);
        }

        public string UserPermissionsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                CONVERT_USER_PERMISSIONS_FILE_NAME);
        }        

        public string RBACControllerSummaryIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                CONVERT_CONTROLLER_RBAC_SUMMARY_FILE_NAME);
        }

        #endregion

        #region RBAC Report

        public string UsersGroupsRolesPermissionsReportFolderPath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                RBAC_FOLDER_NAME);
        }

        public string UsersGroupsRolesPermissionsExcelReportFilePath(JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                REPORT_USERS_GROUPS_ROLES_PERMISSIONS_FILE_NAME,
                this.ProgramOptions.JobName,
                jobTimeRange.From,
                jobTimeRange.To);
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                reportFileName);
        }

        public string UsersReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                RBAC_FOLDER_NAME,
                CONVERT_USERS_FILE_NAME);
        }

        public string GroupsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                RBAC_FOLDER_NAME,
                CONVERT_GROUPS_FILE_NAME);
        }

        public string RolesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                RBAC_FOLDER_NAME,
                CONVERT_ROLES_FILE_NAME);
        }

        public string PermissionsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                RBAC_FOLDER_NAME,
                CONVERT_PERMISSIONS_FILE_NAME);
        }

        public string GroupMembershipsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                RBAC_FOLDER_NAME,
                CONVERT_GROUP_MEMBERSHIPS_FILE_NAME);
        }

        public string RoleMembershipsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                RBAC_FOLDER_NAME,
                CONVERT_ROLE_MEMBERSHIPS_FILE_NAME);
        }

        public string UserPermissionsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                RBAC_FOLDER_NAME,
                CONVERT_USER_PERMISSIONS_FILE_NAME);
        }

        public string RBACControllerSummaryReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                RBAC_FOLDER_NAME,
                CONVERT_CONTROLLER_RBAC_SUMMARY_FILE_NAME);
        }

        public string RBACExcelReportFilePath(JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                REPORT_USERS_GROUPS_ROLES_PERMISSIONS_FILE_NAME,
                this.ProgramOptions.JobName,
                jobTimeRange.From,
                jobTimeRange.To);
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                reportFileName);
        }

        #endregion


        #region Entity Metrics Data

        public string EntityMetricExtractMappingFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.ProgramLocationFolderPath,
                ENTITY_METRICS_EXTRACT_MAPPING_FILE_NAME);
        }

        public string MetricFullRangeDataFilePath(JobTarget jobTarget, string entityFolderName, string metricEntitySubFolderName, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                EXTRACT_METRIC_FULL_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                METRICS_FOLDER_NAME,
                entityFolderName,
                metricEntitySubFolderName,
                reportFileName);
        }

        public string MetricHourRangeDataFilePath(JobTarget jobTarget, string entityFolderName, string metricEntitySubFolderName, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                EXTRACT_METRIC_HOUR_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                METRICS_FOLDER_NAME,
                entityFolderName,
                metricEntitySubFolderName,
                reportFileName);
        }

        #endregion

        #region Entity Metrics Index

        public string EntitiesFullIndexFilePath(JobTarget jobTarget, string entityFolderName)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                METRICS_FOLDER_NAME,
                entityFolderName,
                CONVERT_ENTITIES_METRICS_SUMMARY_FULLRANGE_FILE_NAME);
        }

        public string EntitiesHourIndexFilePath(JobTarget jobTarget, string entityFolderName)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                METRICS_FOLDER_NAME,
                entityFolderName,
                CONVERT_ENTITIES_METRICS_SUMMARY_HOURLY_FILE_NAME);
        }

        public string MetricValuesIndexFilePath(JobTarget jobTarget, string entityFolderName, string metricEntitySubFolderName)
        {
            string reportFileName = String.Format(
                CONVERT_ENTITIES_METRICS_VALUES_FILE_NAME,
                metricEntitySubFolderName);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                METRICS_FOLDER_NAME,
                entityFolderName,
                reportFileName);
        }

        public string MetricsLocationIndexFilePath(JobTarget jobTarget, string entityFolderName)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                METRICS_FOLDER_NAME,
                entityFolderName,
                CONVERT_ENTITIES_METRICS_LOCATIONS_FILE_NAME);
        }

        #endregion

        #region Entity Metrics Report

        public string MetricsReportFolderPath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                METRICS_FOLDER_NAME);
        }

        public string EntitiesFullReportFilePath(string entityFolderName)
        {
            string reportFileName = String.Format(
                CONVERT_ENTITIES_ALL_METRICS_SUMMARY_FULLRANGE_FILE_NAME,
                entityFolderName);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,                
                METRICS_FOLDER_NAME,
                reportFileName);
        }

        public string EntitiesHourReportFilePath(string entityFolderName)
        {
            string reportFileName = String.Format(
                CONVERT_ENTITIES_ALL_METRICS_SUMMARY_HOURLY_FILE_NAME,
                entityFolderName);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                METRICS_FOLDER_NAME,
                reportFileName);
        }

        public string MetricReportFilePath(string entityFolderName, string metricEntitySubFolderName)
        {
            string reportFileName = String.Format(
                CONVERT_ENTITIES_ALL_METRICS_VALUES_FILE_NAME,
                entityFolderName,
                metricEntitySubFolderName);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                METRICS_FOLDER_NAME,
                reportFileName);
        }

        public string MetricReportPerAppFilePath(JobTarget jobTarget, string entityFolderName, string metricEntitySubFolderName)
        {
            string reportFileName = String.Format(
                CONVERT_ENTITIES_ALL_METRICS_VALUES_FILE_NAME,
                entityFolderName,
                metricEntitySubFolderName);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                METRICS_FOLDER_NAME,
                reportFileName);

        }

        public string EntityMetricsExcelReportFilePath(JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                REPORT_METRICS_ALL_ENTITIES_FILE_NAME,
                this.ProgramOptions.JobName,
                jobTimeRange.From,
                jobTimeRange.To);
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                reportFileName);
        }

        #endregion

        #region Entity Metric Graphs Report

        public string EntityTypeMetricGraphsExcelReportFilePath(APMEntityBase entity, JobTarget jobTarget, JobTimeRange jobTimeRange, bool absolutePath)
        {
            string reportFileName = String.Format(
                REPORT_METRICS_GRAPHS_FILE_NAME,
                entity.FolderName,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                jobTimeRange.From,
                jobTimeRange.To);

            string reportFilePath = String.Empty;

            if (absolutePath == true)
            {
                reportFilePath = Path.Combine(
                    this.ProgramOptions.OutputJobFolderPath,
                    REPORT_FOLDER_NAME,
                    getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                    getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                    entity.FolderName,
                    reportFileName);
            }
            else
            {
                reportFilePath = Path.Combine(
                    getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                    getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                    entity.FolderName,
                    reportFileName);
            }

            return reportFilePath;
        }

        #endregion


        #region Entity Flowmap Data

        public string ApplicationFlowmapDataFilePath(JobTarget jobTarget, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                EXTRACT_ENTITY_FLOWMAP_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ACTIVITYGRID_FOLDER_NAME,
                APMApplication.ENTITY_FOLDER,
                reportFileName);
        }

        public string TierFlowmapDataFilePath(JobTarget jobTarget, JobTimeRange jobTimeRange, AppDRESTTier tier)
        {
            string reportFileName = String.Format(
                EXTRACT_ENTITY_FLOWMAP_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ACTIVITYGRID_FOLDER_NAME,
                APMTier.ENTITY_FOLDER,
                getShortenedEntityNameForFileSystem(tier.name, tier.id),
                reportFileName);
        }

        public string TierFlowmapDataFilePath(JobTarget jobTarget, JobTimeRange jobTimeRange, APMTier tier)
        {
            string reportFileName = String.Format(
                EXTRACT_ENTITY_FLOWMAP_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ACTIVITYGRID_FOLDER_NAME,
                APMTier.ENTITY_FOLDER,
                getShortenedEntityNameForFileSystem(tier.TierName, tier.TierID),
                reportFileName);
        }

        public string NodeFlowmapDataFilePath(JobTarget jobTarget, JobTimeRange jobTimeRange, AppDRESTNode node)
        {
            string reportFileName = String.Format(
                EXTRACT_ENTITY_FLOWMAP_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ACTIVITYGRID_FOLDER_NAME,
                APMNode.ENTITY_FOLDER,
                getShortenedEntityNameForFileSystem(node.tierName, node.tierId),
                getShortenedEntityNameForFileSystem(node.name, node.id),
                reportFileName);
        }

        public string NodeFlowmapDataFilePath(JobTarget jobTarget, JobTimeRange jobTimeRange, APMNode node)
        {
            string reportFileName = String.Format(
                EXTRACT_ENTITY_FLOWMAP_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ACTIVITYGRID_FOLDER_NAME,
                APMNode.ENTITY_FOLDER,
                getShortenedEntityNameForFileSystem(node.TierName, node.TierID),
                getShortenedEntityNameForFileSystem(node.NodeName, node.NodeID),
                reportFileName);
        }

        public string BackendFlowmapDataFilePath(JobTarget jobTarget, JobTimeRange jobTimeRange, AppDRESTBackend backend)
        {
            string reportFileName = String.Format(
                EXTRACT_ENTITY_FLOWMAP_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ACTIVITYGRID_FOLDER_NAME,
                Backend.ENTITY_FOLDER,
                getShortenedEntityNameForFileSystem(backend.name, backend.id),
                reportFileName);
        }

        public string BackendFlowmapDataFilePath(JobTarget jobTarget, JobTimeRange jobTimeRange, Backend backend)
        {
            string reportFileName = String.Format(
                EXTRACT_ENTITY_FLOWMAP_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ACTIVITYGRID_FOLDER_NAME,
                Backend.ENTITY_FOLDER,
                getShortenedEntityNameForFileSystem(backend.BackendName, backend.BackendID),
                reportFileName);
        }

        public string BusinessTransactionFlowmapDataFilePath(JobTarget jobTarget, JobTimeRange jobTimeRange, AppDRESTBusinessTransaction businessTransaction)
        {
            string reportFileName = String.Format(
                EXTRACT_ENTITY_FLOWMAP_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ACTIVITYGRID_FOLDER_NAME,
                BusinessTransaction.ENTITY_FOLDER,
                getShortenedEntityNameForFileSystem(businessTransaction.tierName, businessTransaction.tierId),
                getShortenedEntityNameForFileSystem(businessTransaction.name, businessTransaction.id),
                reportFileName);
        }

        public string BusinessTransactionFlowmapDataFilePath(JobTarget jobTarget, JobTimeRange jobTimeRange, BusinessTransaction businessTransaction)
        {
            string reportFileName = String.Format(
                EXTRACT_ENTITY_FLOWMAP_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ACTIVITYGRID_FOLDER_NAME,
                BusinessTransaction.ENTITY_FOLDER,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                reportFileName);
        }

        #endregion

        #region Entity Flowmap Index

        public string ApplicationFlowmapIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ACTIVITYGRID_FOLDER_NAME,
                APMApplication.ENTITY_FOLDER,
                CONVERT_ACTIVITY_GRIDS_FILE_NAME);
        }

        public string ApplicationFlowmapPerMinuteIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ACTIVITYGRID_FOLDER_NAME,
                APMApplication.ENTITY_FOLDER,
                CONVERT_ACTIVITY_GRIDS_PERMINUTE_FILE_NAME);
        }

        public string TiersFlowmapIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ACTIVITYGRID_FOLDER_NAME,
                APMTier.ENTITY_FOLDER,
                CONVERT_ACTIVITY_GRIDS_FILE_NAME);
        }

        public string NodesFlowmapIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ACTIVITYGRID_FOLDER_NAME,
                APMNode.ENTITY_FOLDER,
                CONVERT_ACTIVITY_GRIDS_FILE_NAME);
        }

        public string BackendsFlowmapIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ACTIVITYGRID_FOLDER_NAME,
                Backend.ENTITY_FOLDER,
                CONVERT_ACTIVITY_GRIDS_FILE_NAME);
        }

        public string BusinessTransactionsFlowmapIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                ACTIVITYGRID_FOLDER_NAME,
                BusinessTransaction.ENTITY_FOLDER,
                CONVERT_ACTIVITY_GRIDS_FILE_NAME);
        }

        #endregion

        #region Entity Flowmap Report

        public string ActivityGridReportFolderPath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                ACTIVITYGRID_FOLDER_NAME);
        }

        public string ApplicationsFlowmapReportFilePath()
        {
            string reportFileName = String.Format(
                CONVERT_ALL_ACTIVITY_GRIDS_FILE_NAME,
                APMApplication.ENTITY_FOLDER);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                ACTIVITYGRID_FOLDER_NAME,
                reportFileName);
        }

        public string ApplicationsFlowmapPerMinuteReportFilePath()
        {
            string reportFileName = String.Format(
                CONVERT_ALL_ACTIVITY_GRIDS_PERMINUTE_FILE_NAME,
                APMApplication.ENTITY_FOLDER);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                ACTIVITYGRID_FOLDER_NAME,
                reportFileName);
        }

        public string TiersFlowmapReportFilePath()
        {
            string reportFileName = String.Format(
                CONVERT_ALL_ACTIVITY_GRIDS_FILE_NAME,
                APMTier.ENTITY_FOLDER);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                ACTIVITYGRID_FOLDER_NAME,
                reportFileName);
        }

        public string NodesFlowmapReportFilePath()
        {
            string reportFileName = String.Format(
                CONVERT_ALL_ACTIVITY_GRIDS_FILE_NAME,
                APMNode.ENTITY_FOLDER);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                ACTIVITYGRID_FOLDER_NAME,
                reportFileName);
        }

        public string BackendsFlowmapReportFilePath()
        {
            string reportFileName = String.Format(
                CONVERT_ALL_ACTIVITY_GRIDS_FILE_NAME,
                Backend.ENTITY_FOLDER);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                ACTIVITYGRID_FOLDER_NAME,
                reportFileName);
        }

        public string BusinessTransactionsFlowmapReportFilePath()
        {
            string reportFileName = String.Format(
                CONVERT_ALL_ACTIVITY_GRIDS_FILE_NAME,
                BusinessTransaction.ENTITY_FOLDER);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                ACTIVITYGRID_FOLDER_NAME,
                reportFileName);
        }

        #endregion


        #region Events Data

        public string HealthRuleViolationsDataFilePath(JobTarget jobTarget)
        {
            string reportFileName = String.Format(
                HEALTH_RULE_VIOLATIONS_FILE_NAME,
                this.JobConfiguration.Input.TimeRange.From,
                this.JobConfiguration.Input.TimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                EVENTS_FOLDER_NAME,
                reportFileName);
        }

        public string EventsDataFilePath(JobTarget jobTarget, string eventType)
        {
            string reportFileName = String.Format(
                EVENTS_FILE_NAME,
                eventType,
                this.JobConfiguration.Input.TimeRange.From,
                this.JobConfiguration.Input.TimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                EVENTS_FOLDER_NAME,
                reportFileName);
        }

        public string AuditEventsDataFilePath(JobTarget jobTarget)
        {
            string reportFileName = String.Format(
                AUDIT_EVENTS_FILE_NAME,
                this.JobConfiguration.Input.TimeRange.From,
                this.JobConfiguration.Input.TimeRange.To);
            
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                reportFileName);
        }

        public string NotificationsDataFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                NOTIFICATIONS_FILE_NAME);
        }
        

        #endregion

        #region Events Index

        public string HealthRuleViolationsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                EVENTS_FOLDER_NAME,
                CONVERT_HEALTH_RULE_EVENTS_FILE_NAME);
        }

        public string EventsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                EVENTS_FOLDER_NAME,
                CONVERT_EVENTS_FILE_NAME);
        }

        public string ApplicationEventsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                EVENTS_FOLDER_NAME,
                CONVERT_APPLICATION_EVENTS_FILE_NAME);
        }

        public string AuditEventsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                CONVERT_AUDIT_EVENTS_FILE_NAME);
        }

        #endregion

        #region Events Report

        public string EventsReportFolderPath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                EVENTS_FOLDER_NAME);
        }

        public string HealthRuleViolationsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                EVENTS_FOLDER_NAME,
                CONVERT_HEALTH_RULE_EVENTS_FILE_NAME);
        }

        public string EventsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                EVENTS_FOLDER_NAME,
                CONVERT_EVENTS_FILE_NAME);
        }

        public string AuditEventsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                EVENTS_FOLDER_NAME,
                CONVERT_AUDIT_EVENTS_FILE_NAME);
        }

        public string ApplicationEventsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                EVENTS_FOLDER_NAME,
                CONVERT_APPLICATION_EVENTS_FILE_NAME);
        }

        public string EventsAndHealthRuleViolationsExcelReportFilePath(JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                REPORT_DETECTED_EVENTS_FILE_NAME,
                this.ProgramOptions.JobName,
                jobTimeRange.From,
                jobTimeRange.To);
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                reportFileName);
        }

        #endregion


        #region Snapshots Data

        public string SnapshotsDataFilePath(JobTarget jobTarget, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                EXTRACT_SNAPSHOTS_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                reportFileName);
        }

        public string SnapshotDataFilePath(
            JobTarget jobTarget,
            string tierName, long tierID,
            string businessTransactionName, long businessTransactionID,
            DateTime snapshotTime,
            string userExperience,
            string requestID)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                DATA_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(tierName, tierID),
                getShortenedEntityNameForFileSystem(businessTransactionName, businessTransactionID),
                String.Format(EXTRACT_SNAPSHOT_FILE_NAME, USEREXPERIENCE_FOLDER_MAPPING[userExperience], requestID, snapshotTime));
        }

        public string MethodCallLinesToFrameworkTypetMappingFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.ProgramLocationFolderPath,
                METHOD_CALL_LINES_TO_FRAMEWORK_TYPE_MAPPING_FILE_NAME);
        }

        #endregion

        #region Snapshots Index

        #region Snapshots Business Transaction for Time Range

        public string SnapshotsIndexBusinessTransactionHourRangeFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                CONVERT_SNAPSHOTS_TIMERANGE_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                reportFileName);
        }

        public string SnapshotsSegmentsIndexBusinessTransactionHourRangeFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                CONVERT_SNAPSHOTS_SEGMENTS_TIMERANGE_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                reportFileName);
        }

        public string SnapshotsExitCallsIndexBusinessTransactionHourRangeFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                CONVERT_SNAPSHOTS_SEGMENTS_EXIT_CALLS_TIMERANGE_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                reportFileName);
        }

        public string SnapshotsServiceEndpointCallsIndexBusinessTransactionHourRangeFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                CONVERT_SNAPSHOTS_SEGMENTS_SERVICE_ENDPOINTS_CALLS_TIMERANGE_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                reportFileName);
        }

        public string SnapshotsDetectedErrorsIndexBusinessTransactionHourRangeFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                CONVERT_SNAPSHOTS_SEGMENTS_DETECTED_ERRORS_TIMERANGE_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                reportFileName);
        }

        public string SnapshotsBusinessDataIndexBusinessTransactionHourRangeFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                CONVERT_SNAPSHOTS_SEGMENTS_BUSINESS_DATA_TIMERANGE_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                reportFileName);
        }

        public string SnapshotsMethodCallLinesIndexBusinessTransactionHourRangeFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                CONVERT_SNAPSHOTS_SEGMENTS_METHOD_CALL_LINES_FILE_TIMERANGE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                reportFileName);
        }

        public string SnapshotsMethodCallLinesOccurrencesIndexBusinessTransactionHourRangeFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                CONVERT_SNAPSHOTS_SEGMENTS_METHOD_CALL_LINES_OCCURRENCES_TIMERANGE_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                reportFileName);
        }

        #endregion

        #region Snapshots Business Transaction

        public string SnapshotsIndexBusinessTransactionFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                CONVERT_SNAPSHOTS_FILE_NAME);
        }

        public string SnapshotsSegmentsIndexBusinessTransactionFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                CONVERT_SNAPSHOTS_SEGMENTS_FILE_NAME);
        }

        public string SnapshotsExitCallsIndexBusinessTransactionFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                CONVERT_SNAPSHOTS_SEGMENTS_EXIT_CALLS_FILE_NAME);
        }

        public string SnapshotsServiceEndpointCallsIndexBusinessTransactionFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                CONVERT_SNAPSHOTS_SEGMENTS_SERVICE_ENDPOINTS_CALLS_FILE_NAME);
        }

        public string SnapshotsDetectedErrorsIndexBusinessTransactionFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                CONVERT_SNAPSHOTS_SEGMENTS_DETECTED_ERRORS_FILE_NAME);
        }

        public string SnapshotsBusinessDataIndexBusinessTransactionFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                CONVERT_SNAPSHOTS_SEGMENTS_BUSINESS_DATA_FILE_NAME);
        }

        public string SnapshotsMethodCallLinesIndexBusinessTransactionFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                CONVERT_SNAPSHOTS_SEGMENTS_METHOD_CALL_LINES_FILE_NAME);
        }

        public string SnapshotsMethodCallLinesOccurrencesIndexBusinessTransactionFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                CONVERT_SNAPSHOTS_SEGMENTS_METHOD_CALL_LINES_OCCURRENCES_FILE_NAME);
        }

        #endregion

        #region Snapshots All

        public string SnapshotsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                CONVERT_SNAPSHOTS_FILE_NAME);
        }

        public string SnapshotsSegmentsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                CONVERT_SNAPSHOT_SEGMENTS_FILE_NAME);
        }

        public string SnapshotsExitCallsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                CONVERT_SNAPSHOTS_SEGMENTS_EXIT_CALLS_FILE_NAME);
        }

        public string SnapshotsServiceEndpointCallsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                CONVERT_SNAPSHOTS_SEGMENTS_SERVICE_ENDPOINTS_CALLS_FILE_NAME);
        }

        public string SnapshotsDetectedErrorsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                CONVERT_SNAPSHOTS_SEGMENTS_DETECTED_ERRORS_FILE_NAME);
        }

        public string SnapshotsBusinessDataIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                CONVERT_SNAPSHOTS_SEGMENTS_BUSINESS_DATA_FILE_NAME);
        }

        public string SnapshotsMethodCallLinesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                CONVERT_SNAPSHOTS_SEGMENTS_METHOD_CALL_LINES_FILE_NAME);
        }

        public string SnapshotsMethodCallLinesOccurrencesIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                CONVERT_SNAPSHOTS_SEGMENTS_METHOD_CALL_LINES_OCCURRENCES_FILE_NAME);
        }

        #endregion

        #region Snapshots Folded Call Stacks All

        public string SnapshotsFoldedCallStacksIndexBusinessTransactionHourRangeFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                CONVERT_SNAPSHOTS_SEGMENTS_FOLDED_CALL_STACKS_TIMERANGE_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                reportFileName);
        }

        public string SnapshotsFoldedCallStacksIndexBusinessTransactionNodeHourRangeFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction, APMNode node, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                CONVERT_SNAPSHOTS_SEGMENTS_FOLDED_CALL_STACKS_TIMERANGE_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                APMNode.ENTITY_FOLDER,
                getShortenedEntityNameForFileSystem(node.NodeName, node.NodeID),
                reportFileName);
        }

        public string SnapshotsFoldedCallStacksWithTimeIndexBusinessTransactionHourRangeFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                CONVERT_SNAPSHOTS_SEGMENTS_FOLDED_CALL_STACKS_WITH_TIME_TIMERANGE_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                reportFileName);
        }

        public string SnapshotsFoldedCallStacksWithTimeIndexBusinessTransactionNodeHourRangeFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction, APMNode node, JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                CONVERT_SNAPSHOTS_SEGMENTS_FOLDED_CALL_STACKS_WITH_TIME_TIMERANGE_FILE_NAME,
                jobTimeRange.From,
                jobTimeRange.To);

            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                APMNode.ENTITY_FOLDER,
                getShortenedEntityNameForFileSystem(node.NodeName, node.NodeID),
                reportFileName);
        }

        public string SnapshotsFoldedCallStacksIndexApplicationFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                CONVERT_SNAPSHOTS_SEGMENTS_FOLDED_CALL_STACKS_FILE_NAME);
        }

        public string SnapshotsFoldedCallStacksIndexEntityFilePath(JobTarget jobTarget, APMTier tier)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(tier.TierName, tier.TierID),
                CONVERT_SNAPSHOTS_SEGMENTS_FOLDED_CALL_STACKS_FILE_NAME);
        }

        public string SnapshotsFoldedCallStacksIndexEntityFilePath(JobTarget jobTarget, APMNode node)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                APMNode.ENTITY_FOLDER,
                getShortenedEntityNameForFileSystem(node.TierName, node.TierID),
                getShortenedEntityNameForFileSystem(node.NodeName, node.NodeID),
                CONVERT_SNAPSHOTS_SEGMENTS_FOLDED_CALL_STACKS_FILE_NAME);
        }

        public string SnapshotsFoldedCallStacksIndexEntityFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                CONVERT_SNAPSHOTS_SEGMENTS_FOLDED_CALL_STACKS_FILE_NAME);
        }

        public string SnapshotsFoldedCallStacksWithTimeIndexApplicationFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                CONVERT_SNAPSHOTS_SEGMENTS_FOLDED_CALL_STACKS_WITH_TIME_FILE_NAME);
        }

        public string SnapshotsFoldedCallStacksWithTimeIndexEntityFilePath(JobTarget jobTarget, APMTier tier)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(tier.TierName, tier.TierID),
                CONVERT_SNAPSHOTS_SEGMENTS_FOLDED_CALL_STACKS_WITH_TIME_FILE_NAME);
        }

        public string SnapshotsFoldedCallStacksWithTimeIndexEntityFilePath(JobTarget jobTarget, APMNode node)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                APMNode.ENTITY_FOLDER,
                getShortenedEntityNameForFileSystem(node.TierName, node.TierID),
                getShortenedEntityNameForFileSystem(node.NodeName, node.NodeID),
                CONVERT_SNAPSHOTS_SEGMENTS_FOLDED_CALL_STACKS_WITH_TIME_FILE_NAME);
        }

        public string SnapshotsFoldedCallStacksWithTimeIndexEntityFilePath(JobTarget jobTarget, BusinessTransaction businessTransaction)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                getShortenedEntityNameForFileSystem(businessTransaction.TierName, businessTransaction.TierID),
                getShortenedEntityNameForFileSystem(businessTransaction.BTName, businessTransaction.BTID),
                CONVERT_SNAPSHOTS_SEGMENTS_FOLDED_CALL_STACKS_WITH_TIME_FILE_NAME);
        }

        #endregion

        public string ApplicationSnapshotsIndexFilePath(JobTarget jobTarget)
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                INDEX_FOLDER_NAME,
                getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                getShortenedEntityNameForFileSystem(jobTarget.Application, jobTarget.ApplicationID),
                SNAPSHOTS_FOLDER_NAME,
                CONVERT_APPLICATION_SNAPSHOTS_FILE_NAME);
        }

        #endregion

        #region Snapshots Report

        public string SnapshotsReportFolderPath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                SNAPSHOTS_FOLDER_NAME);
        }

        public string SnapshotsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                SNAPSHOTS_FOLDER_NAME,
                CONVERT_SNAPSHOTS_FILE_NAME);
        }

        public string SnapshotsSegmentsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                SNAPSHOTS_FOLDER_NAME,
                CONVERT_SNAPSHOTS_SEGMENTS_FILE_NAME);
        }

        public string SnapshotsExitCallsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                SNAPSHOTS_FOLDER_NAME,
                CONVERT_SNAPSHOTS_SEGMENTS_EXIT_CALLS_FILE_NAME);
        }

        public string SnapshotsServiceEndpointCallsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                SNAPSHOTS_FOLDER_NAME,
                CONVERT_SNAPSHOTS_SEGMENTS_SERVICE_ENDPOINTS_CALLS_FILE_NAME);
        }

        public string SnapshotsDetectedErrorsCallsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                SNAPSHOTS_FOLDER_NAME,
                CONVERT_SNAPSHOTS_SEGMENTS_DETECTED_ERRORS_FILE_NAME);
        }

        public string SnapshotsBusinessDataReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                SNAPSHOTS_FOLDER_NAME,
                CONVERT_SNAPSHOTS_SEGMENTS_BUSINESS_DATA_FILE_NAME);
        }

        public string SnapshotsMethodCallLinesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                SNAPSHOTS_FOLDER_NAME,
                CONVERT_SNAPSHOTS_SEGMENTS_METHOD_CALL_LINES_FILE_NAME);
        }

        public string SnapshotsMethodCallLinesOccurrencesReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                SNAPSHOTS_FOLDER_NAME,
                CONVERT_SNAPSHOTS_SEGMENTS_METHOD_CALL_LINES_OCCURRENCES_FILE_NAME);
        }

        public string ApplicationSnapshotsReportFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                SNAPSHOTS_FOLDER_NAME,
                CONVERT_APPLICATION_SNAPSHOTS_FILE_NAME);
        }

        public string SnapshotsExcelReportFilePath(JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                REPORT_SNAPSHOTS_FILE_NAME,
                this.ProgramOptions.JobName,
                jobTimeRange.From,
                jobTimeRange.To);
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                reportFileName);
        }

        public string SnapshotMethodCallsExcelReportFilePath(JobTimeRange jobTimeRange)
        {
            string reportFileName = String.Format(
                REPORT_SNAPSHOTS_METHOD_CALL_LINES_FILE_NAME,
                this.ProgramOptions.JobName,
                jobTimeRange.From,
                jobTimeRange.To);
            return Path.Combine(
                this.ProgramOptions.OutputJobFolderPath,
                REPORT_FOLDER_NAME,
                reportFileName);
        }

        #endregion


        #region Flame Graph Report

        public string FlameGraphTemplateFilePath()
        {
            return Path.Combine(
                this.ProgramOptions.ProgramLocationFolderPath,
                FLAME_GRAPH_TEMPLATE_FILE_NAME);
        }

        public string FlameGraphReportFilePath(APMEntityBase entity, JobTarget jobTarget, JobTimeRange jobTimeRange, bool absolutePath)
        {
            string reportFileName = String.Empty;
            string reportFilePath = String.Empty;

            if (entity is APMApplication)
            {
                reportFileName = String.Format(
                    REPORT_FLAME_GRAPH_APPLICATION_FILE_NAME,
                    getFileSystemSafeString(new Uri(entity.Controller).Host),
                    getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                    jobTimeRange.From,
                    jobTimeRange.To);
            }
            else if (entity is APMTier)
            {
                reportFileName = String.Format(
                    REPORT_FLAME_GRAPH_TIER_FILE_NAME,
                    getFileSystemSafeString(new Uri(entity.Controller).Host),
                    getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                    getShortenedEntityNameForFileSystem(entity.EntityName, entity.EntityID),
                    jobTimeRange.From,
                    jobTimeRange.To);
            }
            else if (entity is APMNode)
            {
                reportFileName = String.Format(
                    REPORT_FLAME_GRAPH_NODE_FILE_NAME,
                    getFileSystemSafeString(new Uri(entity.Controller).Host),
                    getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                    getShortenedEntityNameForFileSystem(entity.EntityName, entity.EntityID),
                    jobTimeRange.From,
                    jobTimeRange.To);
            }
            else if (entity is BusinessTransaction)
            {
                reportFileName = String.Format(
                    REPORT_FLAME_GRAPH_BUSINESS_TRANSACTION_FILE_NAME,
                    getFileSystemSafeString(new Uri(entity.Controller).Host),
                    getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                    getShortenedEntityNameForFileSystem(entity.EntityName, entity.EntityID),
                    jobTimeRange.From,
                    jobTimeRange.To);
            }

            if (reportFileName.Length > 0)
            {
                if (absolutePath == true)
                {
                    reportFilePath = Path.Combine(
                        this.ProgramOptions.OutputJobFolderPath,
                        REPORT_FOLDER_NAME,
                        getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                        getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                        entity.FolderName,
                        reportFileName);
                }
                else
                {
                    reportFilePath = Path.Combine(
                        getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                        getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                        entity.FolderName,
                        reportFileName);
                }
            }

            return reportFilePath;
        }

        public string FlameChartReportFilePath(APMEntityBase entity, JobTarget jobTarget, JobTimeRange jobTimeRange, bool absolutePath)
        {
            string reportFileName = String.Empty;
            string reportFilePath = String.Empty;

            if (entity is APMApplication)
            {
                reportFileName = String.Format(
                    REPORT_FLAME_CHART_APPLICATION_FILE_NAME,
                    getFileSystemSafeString(new Uri(entity.Controller).Host),
                    getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                    jobTimeRange.From,
                    jobTimeRange.To);
            }
            else if (entity is APMTier)
            {
                reportFileName = String.Format(
                    REPORT_FLAME_CHART_TIER_FILE_NAME,
                    getFileSystemSafeString(new Uri(entity.Controller).Host),
                    getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                    getShortenedEntityNameForFileSystem(entity.EntityName, entity.EntityID),
                    jobTimeRange.From,
                    jobTimeRange.To);
            }
            else if (entity is APMNode)
            {
                reportFileName = String.Format(
                    REPORT_FLAME_CHART_NODE_FILE_NAME,
                    getFileSystemSafeString(new Uri(entity.Controller).Host),
                    getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                    getShortenedEntityNameForFileSystem(entity.EntityName, entity.EntityID),
                    jobTimeRange.From,
                    jobTimeRange.To);
            }
            else if (entity is BusinessTransaction)
            {
                reportFileName = String.Format(
                    REPORT_FLAME_CHART_BUSINESS_TRANSACTION_FILE_NAME,
                    getFileSystemSafeString(new Uri(entity.Controller).Host),
                    getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                    getShortenedEntityNameForFileSystem(entity.EntityName, entity.EntityID),
                    jobTimeRange.From,
                    jobTimeRange.To);
            }

            if (reportFileName.Length > 0)
            {
                if (absolutePath == true)
                {
                    reportFilePath = Path.Combine(
                        this.ProgramOptions.OutputJobFolderPath,
                        REPORT_FOLDER_NAME,
                        getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                        getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                        entity.FolderName,
                        reportFileName);
                }
                else
                {
                    reportFilePath = Path.Combine(
                        getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                        getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                        entity.FolderName,
                        reportFileName);
                }
            }

            return reportFilePath;
        }

        public string FlameGraphReportFilePath(Snapshot snapshot, JobTarget jobTarget, bool absolutePath)
        {
            string reportFileName = String.Format(
                REPORT_FLAME_GRAPH_SNAPSHOT_FILE_NAME,
                snapshot.UserExperience,
                snapshot.OccurredUtc,
                snapshot.RequestID);

            string reportFilePath = String.Empty;

            if (absolutePath == true)
            {
                reportFilePath = Path.Combine(
                    this.ProgramOptions.OutputJobFolderPath,
                    REPORT_FOLDER_NAME,
                    getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                    getShortenedEntityNameForFileSystem(snapshot.ApplicationName, snapshot.ApplicationID),
                    SNAPSHOTS_FOLDER_NAME,
                    getShortenedEntityNameForFileSystem(snapshot.TierName, snapshot.TierID),
                    getShortenedEntityNameForFileSystem(snapshot.BTName, snapshot.BTID),
                    reportFileName);
            }
            else
            {
                reportFilePath = Path.Combine(
                    getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                    getShortenedEntityNameForFileSystem(snapshot.ApplicationName, snapshot.ApplicationID),
                    SNAPSHOTS_FOLDER_NAME,
                    getShortenedEntityNameForFileSystem(snapshot.TierName, snapshot.TierID),
                    getShortenedEntityNameForFileSystem(snapshot.BTName, snapshot.BTID),
                    reportFileName);
            }

            return reportFilePath;
        }

        #endregion


        #region Entity Details Report

        public string EntityMetricAndDetailExcelReportFilePath(APMEntityBase entity, JobTarget jobTarget, JobTimeRange jobTimeRange, bool absolutePath)
        {
            string reportFileName = String.Empty;
            string reportFilePath = String.Empty;

            if (entity is APMApplication)
            {
                reportFileName = String.Format(
                    REPORT_ENTITY_DETAILS_APPLICATION_FILE_NAME,
                    getFileSystemSafeString(new Uri(entity.Controller).Host),
                    getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                    jobTimeRange.From,
                    jobTimeRange.To);
            }
            else if (entity is APMTier)
            {
                reportFileName = String.Format(
                    REPORT_ENTITY_DETAILS_ENTITY_FILE_NAME,
                    getFileSystemSafeString(new Uri(entity.Controller).Host),
                    getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                    getShortenedEntityNameForFileSystem(entity.EntityName, entity.EntityID),
                    jobTimeRange.From,
                    jobTimeRange.To);
            }
            else if (entity is APMNode)
            {
                reportFileName = String.Format(
                    REPORT_ENTITY_DETAILS_ENTITY_FILE_NAME,
                    getFileSystemSafeString(new Uri(entity.Controller).Host),
                    getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                    getShortenedEntityNameForFileSystem(entity.EntityName, entity.EntityID),
                    jobTimeRange.From,
                    jobTimeRange.To);
            }
            else if (entity is Backend)
            {
                reportFileName = String.Format(
                    REPORT_ENTITY_DETAILS_ENTITY_FILE_NAME,
                    getFileSystemSafeString(new Uri(entity.Controller).Host),
                    getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                    getShortenedEntityNameForFileSystem(entity.EntityName, entity.EntityID),
                    jobTimeRange.From,
                    jobTimeRange.To);
            }
            else if (entity is BusinessTransaction)
            {
                reportFileName = String.Format(
                    REPORT_ENTITY_DETAILS_ENTITY_FILE_NAME,
                    getFileSystemSafeString(new Uri(entity.Controller).Host),
                    getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                    getShortenedEntityNameForFileSystem(entity.EntityName, entity.EntityID),
                    jobTimeRange.From,
                    jobTimeRange.To);
            }
            else if (entity is ServiceEndpoint)
            {
                reportFileName = String.Format(
                    REPORT_ENTITY_DETAILS_ENTITY_FILE_NAME,
                    getFileSystemSafeString(new Uri(entity.Controller).Host),
                    getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                    getShortenedEntityNameForFileSystem(entity.EntityName, entity.EntityID),
                    jobTimeRange.From,
                    jobTimeRange.To);
            }
            else if (entity is Error)
            {
                reportFileName = String.Format(
                    REPORT_ENTITY_DETAILS_ENTITY_FILE_NAME,
                    getFileSystemSafeString(new Uri(entity.Controller).Host),
                    getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                    getShortenedEntityNameForFileSystem(entity.EntityName, entity.EntityID),
                    jobTimeRange.From,
                    jobTimeRange.To);
            }

            if (reportFileName.Length > 0)
            {
                if (absolutePath == true)
                {
                    reportFilePath = Path.Combine(
                        this.ProgramOptions.OutputJobFolderPath,
                        REPORT_FOLDER_NAME,
                        getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                        getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                        entity.FolderName,
                        reportFileName);
                }
                else
                {
                    reportFilePath = Path.Combine(
                        getFileSystemSafeString(new Uri(jobTarget.Controller).Host),
                        getShortenedEntityNameForFileSystem(entity.ApplicationName, entity.ApplicationID),
                        entity.FolderName,
                        reportFileName);
                }
            }

            return reportFilePath;
        }

        #endregion


        #region Helper function for various entity naming

        public static string getFileSystemSafeString(string fileOrFolderNameToClear)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                fileOrFolderNameToClear = fileOrFolderNameToClear.Replace(c, '-');
            }

            return fileOrFolderNameToClear;
        }

        public static string getShortenedEntityNameForFileSystem(string entityName, long entityID)
        {
            string originalEntityName = entityName;

            // First, strip out unsafe characters
            entityName = getFileSystemSafeString(entityName);

            // Second, shorten the string 
            if (entityName.Length > 12) entityName = entityName.Substring(0, 12);

            return String.Format("{0}.{1}", entityName, entityID);
        }

        #endregion
    }
}
