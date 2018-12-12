using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainSQL.Processing {
    public class BizLogicFactory {

        public static IBlockFileReader NewBlockFileReader(string blockFilePath) {
            return new BlockFileReader(blockFilePath);
        }


        public static IDifficultyCalculator NewDifficultyCalculator() {
            return new DifficultyCalculator();
        }

        public static IBlockOrganizer NewBlockOrganizer(bool batchProcessing) {
            return new OptimizedBlockOrganizer(batchProcessing);
        }

        public static INodeCommunicator NewCommunicator(NodeEndpoint endpoint) {
            return new NodeCommunicator(endpoint);
        }

        public static IBlockStream NewNodeBlockStream(NodeEndpoint endpoint) {
            return new NodeBlockStream( NewCommunicator(endpoint), NewBlockLocator());
        }

        public static IBlockStream NewBlockStream(string directory) {
            if (!Directory.Exists(directory)) throw new ArgumentException("Directory not found", nameof(directory));
            var indexDB = Path.Combine(directory, "index");
            if (Directory.Exists(indexDB) && ProcessingTierHelper.ValidateLevelDBDirectory(indexDB).Success) {
                return new IndexedFilesBlockStream(directory, indexDB);
            }            
            return new UnindexedFilesBlockStream(directory);
        }

        public static IBlockStreamParser NewBLKFileStreamParser(IBlockStream stream, IBlockLocator locator,  IPreProcessor preProcessor, IPostProcessor postProcessor, IBlockStreamPersistor persistor) {
            return new ParallelizedBlockStreamParser(stream, locator, preProcessor, postProcessor, persistor);
        }

        public static IBlockStreamParser NewNodeStreamParser(IBlockStream stream, IBlockLocator locator, IPreProcessor preProcessor, IPostProcessor postProcessor, IBlockStreamPersistor persistor) {
            return new SequentialBlockStreamParser(stream, locator, preProcessor, postProcessor, persistor);
        }

        public static IBlockStreamPersistor NewBlockStreamPersistor() {
            return new BlockStreamPersistor();
        }

        public static IPreProcessor NewPreProcessor(bool optimizeForBulkLoading, bool expandScripts) {
            return new StandardPreProcessor(optimizeForBulkLoading, expandScripts);
        }

        public static IPostProcessor NewPostProcessor() {
            return new PostProcessor();
        }

        public static IBlockLocator NewBlockLocator() {
            return new BlockLocator();
        }

    }
}
